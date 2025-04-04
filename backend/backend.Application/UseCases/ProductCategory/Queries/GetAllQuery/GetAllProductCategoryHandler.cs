// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.ProductCategory.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.ProductCategory.Queries.GetAllQuery;

public class GetAllProductCategoryHandler : IRequestHandler<GetAllProductCategoryQuery, BaseResponse<IEnumerable<ProductCategoryResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllProductCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<ProductCategoryResponseDto>>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProductCategoryResponseDto>>();

        try
        {
            var products = _unitOfWork.ProductCategory.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        products = products.Where(x => x.Categoryname.Contains(request.TextFilter));
                        break;
                    case 2:
                        products = products.Where(x => x.Description!.Contains(request.TextFilter));
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

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, products)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await products.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<ProductCategoryResponseDto>>(items);
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