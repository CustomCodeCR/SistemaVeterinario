// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Inventory.Commands.CreateCommand;

public class CreateInventoryCommand : IRequest<BaseResponse<bool>>
{
    public int InventoryId { get; set; }
    public int Productid { get; set; }
    public int Quantity { get; set; } 
     public DateTime Updatedate { get; set; }
    public int AuditCreateUser { get; set; }
}