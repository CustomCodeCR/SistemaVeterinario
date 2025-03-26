using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.PurchaseOrder.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.PurchaseOrder.Queries.GetAllQuery;

public class GetAllPurchaseOrderHandler : IRequestHandler<GetAllPurchaseOrderQuery, BaseResponse<IEnumerable<PurchaseOrderResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllPurchaseOrderHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<PurchaseOrderResponseDto>>> Handle(GetAllPurchaseOrderQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PurchaseOrderResponseDto>>();

        try
        {
            var appointments = _unitOfWork.PurchaseOrder.GetAllQueryable()
                .Include(x => x.Supplier)
                .AsQueryable(); ;

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        appointments = appointments.Where(x => x.Supplier.Name.Contains(request.TextFilter));
                        break;
                    case 2:
                        appointments = appointments.Where(x => x.Status!.Contains(request.TextFilter));
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
            response.Data = _mapper.Map<IEnumerable<PurchaseOrderResponseDto>>(items);
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