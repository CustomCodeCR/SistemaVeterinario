// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Sale.Response;
using MediatR;

namespace backend.Application.UseCases.Sale.Queries.GetByIdQuery;

public class GetSaleByIdQuery : IRequest<BaseResponse<SaleByIdResponseDto>>
{
    public int SaleId { get; set; }
}