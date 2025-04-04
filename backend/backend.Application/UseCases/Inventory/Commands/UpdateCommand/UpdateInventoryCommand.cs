// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Inventory.Commands.UpdateCommand;

public class UpdateInventoryCommand : IRequest<BaseResponse<bool>>
{
    public int InventoryId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime UpdateDate { get; set; }
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}