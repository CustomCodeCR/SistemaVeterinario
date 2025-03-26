// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Supplier.Commands.DeleteCommand;

public class DeleteSupplierCommand : IRequest<BaseResponse<bool>>
{
    public int SupplierId { get; set; }
    public int AuditDeleteUser { get; set; }
}