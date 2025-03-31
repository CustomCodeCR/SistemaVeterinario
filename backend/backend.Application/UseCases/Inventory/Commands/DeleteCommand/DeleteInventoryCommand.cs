// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Inventory.Commands.DeleteCommand;

public class DeleteInventoryCommand : IRequest<BaseResponse<bool>>
{
    public int InventoryId { get; set; }
    public int AuditDeleteUser { get; set; }
}