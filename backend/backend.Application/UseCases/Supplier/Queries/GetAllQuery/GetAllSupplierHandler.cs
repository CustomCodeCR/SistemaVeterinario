// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Supplier.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.Supplier.Queries.GetAllQuery;

public class GetAllSupplierHandler : IRequestHandler<GetAllSupplierQuery, BaseResponse<IEnumerable<SupplierResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

public GetAllSupplierHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _ordering = ordering;
}

public async Task<BaseResponse<IEnumerable<SupplierResponseDto>>> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
{
    var response = new BaseResponse<IEnumerable<SupplierResponseDto>>();

    try
    {
        var suppliers = _unitOfWork.Supplier.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
        {
            switch (request.NumFilter)
            {
                case 1:
                    suppliers = suppliers.Where(x => x.Name.Contains(request.TextFilter));
                    break;
                case 2:
                    suppliers = suppliers.Where(x => x.Contact!.Contains(request.TextFilter));
                    break;
            }
        }

        if (request.StateFilter is not null)
        {
            suppliers = suppliers.Where(x => x.State == request.StateFilter);
        }

        if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
        {
            suppliers = suppliers.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
        }

        request.Sort ??= "Id";

        var items = await _ordering.Ordering(request, suppliers)
            .ToListAsync(cancellationToken);

        response.IsSuccess = true;
        response.TotalRecords = await suppliers.CountAsync(cancellationToken);
        response.Data = _mapper.Map<IEnumerable<SupplierResponseDto>>(items);
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