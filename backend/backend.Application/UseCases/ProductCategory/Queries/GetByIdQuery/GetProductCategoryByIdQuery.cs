// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------
using backend.Application.Commons.Bases;
using backend.Application.Dtos.ProductCategory.Response;
using MediatR;

namespace backend.Application.UseCases.ProductCategory.Queries.GetByIdQuery
{
    public class GetProductCategoryByIdQuery 
        : IRequest<BaseResponse<ProductCategoryByIdResponseDto>>
    {
        public int CategoryId { get; set; }
    }
}