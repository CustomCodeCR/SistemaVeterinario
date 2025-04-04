using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Sale.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.Sale.Queries.GetAllQuery;

public class GetAllSaleHandler : IRequestHandler<GetAllSaleQuery, BaseResponse<IEnumerable<SaleResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllSaleHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<SaleResponseDto>>> Handle(GetAllSaleQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SaleResponseDto>>();

        try
        {
            var appointments = _unitOfWork.Sale.GetAllQueryable()
                .Include(x => x.Client)
                .Include(x => x.Client.User)
                .AsQueryable(); ;

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        appointments = appointments.Where(x => x.Client.User.Firstname.Contains(request.TextFilter));
                        break;
                    case 2:
                        appointments = appointments.Where(x => x.Client.User.Lastname.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                appointments = appointments.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                appointments = appointments.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                         x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, appointments)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await appointments.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<SaleResponseDto>>(items);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}