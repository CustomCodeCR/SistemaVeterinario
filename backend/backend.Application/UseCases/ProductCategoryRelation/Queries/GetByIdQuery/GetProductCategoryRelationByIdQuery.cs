// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.ProductCategoryRelation.Response;
using MediatR;

namespace backend.Application.UseCases.ProductCategoryRelation.Queries.GetByIdQuery;

public class GetProductCategoryRelationByIdQuery : IRequest<BaseResponse<ProductCategoryRelationByIdResponseDto>>
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
}