using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Inventory.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.Inventory.Queries.GetAllQuery;

public class GetAllInventoryHandler : IRequestHandler<GetAllInventoryQuery, BaseResponse<IEnumerable<InventoryResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

public GetAllInventoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _ordering = ordering;
}

public async Task<BaseResponse<IEnumerable<InventoryResponseDto>>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
{
    var response = new BaseResponse<IEnumerable<InventoryResponseDto>>();

    try
    {
        var Inventorys = _unitOfWork.Inventory.GetAllQueryable()
                .Include(x => x.User)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
        {
            switch (request.NumFilter)
            {
                case 1:
                    Inventorys = Inventorys.Where(x => x.Address.Contains(request.TextFilter));
                    break;
                case 2:
                    Inventorys = Inventorys.Where(x => x.Phone!.Contains(request.TextFilter));
                    break;
            }
        }

        if (request.StateFilter is not null)
        {
            Inventorys = Inventorys.Where(x => x.State == request.StateFilter);
        }

        if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
        {
            Inventorys = Inventorys.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
        }

        request.Sort ??= "Id";

        var items = await _ordering.Ordering(request, Inventorys)
            .ToListAsync(cancellationToken);

        response.IsSuccess = true;
        response.TotalRecords = await Inventorys.CountAsync(cancellationToken);
        response.Data = _mapper.Map<IEnumerable<InventoryResponseDto>>(items);
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