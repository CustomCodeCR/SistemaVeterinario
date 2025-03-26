// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Product.Response;
using MediatR;

namespace backend.Application.UseCases.Product.Queries.GetByIdQuery;

public class GetProductByIdQuery : IRequest<BaseResponse<ProductByIdResponseDto>>
{
    public int ProductId { get; set; }
}