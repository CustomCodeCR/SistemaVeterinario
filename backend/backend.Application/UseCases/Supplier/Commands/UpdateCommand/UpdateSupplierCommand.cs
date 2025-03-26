// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Supplier.Commands.UpdateCommand;

public class UpdateSupplierCommand : IRequest<BaseResponse<bool>>
{
    public int SupplierId { get; set; }
    public string Name { get; set; } = null!;
    public string Contact { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}