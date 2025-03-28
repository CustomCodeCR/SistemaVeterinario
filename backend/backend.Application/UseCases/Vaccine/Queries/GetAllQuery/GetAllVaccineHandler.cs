using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Vaccine.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.Vaccine.Queries.GetAllQuery;

public class GetAllVaccineHandler : IRequestHandler<GetAllVaccineQuery, BaseResponse<IEnumerable<VaccineResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

public GetAllVaccineHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _ordering = ordering;
}

public async Task<BaseResponse<IEnumerable<VaccineResponseDto>>> Handle(GetAllVaccineQuery request, CancellationToken cancellationToken)
{
    var response = new BaseResponse<IEnumerable<VaccineResponseDto>>();

    try
    {
        var clients = _unitOfWork.Vaccine.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
        {
            switch (request.NumFilter)
            {
                case 1:
                    clients = clients.Where(x => x.Vaccinename.Contains(request.TextFilter));
                    break;
                case 2:
                    clients = clients.Where(x => x.Type!.Contains(request.TextFilter));
                    break;
            }
        }

        if (request.StateFilter is not null)
        {
            clients = clients.Where(x => x.State == request.StateFilter);
        }

        if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
        {
            clients = clients.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
        }

        request.Sort ??= "Id";

        var items = await _ordering.Ordering(request, clients)
            .ToListAsync(cancellationToken);

        response.IsSuccess = true;
        response.TotalRecords = await clients.CountAsync(cancellationToken);
        response.Data = _mapper.Map<IEnumerable<VaccineResponseDto>>(items);
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