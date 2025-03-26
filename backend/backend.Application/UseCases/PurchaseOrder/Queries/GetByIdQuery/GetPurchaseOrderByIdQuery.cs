// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.PurchaseOrder.Response;
using MediatR;

namespace backend.Application.UseCases.PurchaseOrder.Queries.GetByIdQuery;

public class GetPurchaseOrderByIdQuery : IRequest<BaseResponse<PurchaseOrderByIdResponseDto>>
{
    public int PurchaseOrderId { get; set; }
}