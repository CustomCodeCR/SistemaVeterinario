// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.ProductCategoryRelation.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.ProductCategoryRelation.Queries.GetAllQuery;

public class GetAllProductCategoryRelationHandler : IRequestHandler<GetAllProductCategoryRelationQuery, BaseResponse<IEnumerable<ProductCategoryRelationResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllProductCategoryRelationHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<ProductCategoryRelationResponseDto>>> Handle(GetAllProductCategoryRelationQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProductCategoryRelationResponseDto>>();

        try
        {
            var products = _unitOfWork.ProductCategoryRelation.GetAllQueryable()
                .Include(x => x.Product)
                .Include(x => x.Category)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 0:
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                products = products.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                products = products.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                         x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Productid";

            var items = await _ordering.Ordering(request, products)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await products.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<ProductCategoryRelationResponseDto>>(items);
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