// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Supplier.Response;
using MediatR;

namespace backend.Application.UseCases.Supplier.Queries.GetByIdQuery;

public class GetSupplierByIdQuery : IRequest<BaseResponse<SupplierByIdResponseDto>>
{
    public int SupplierId { get; set; }
}