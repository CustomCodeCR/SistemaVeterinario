// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.PurchaseOrder.Commands.CreateCommand;

public class CreatePurchaseOrderCommand : IRequest<BaseResponse<bool>>
{
    public int SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public int State { get; set; }
    public int AuditCreateUser {  get; set; }
    public IEnumerable<CreatePurchaseOrderDetailCommand> PurchaseOrderDetail { get; set; } = null!;
}

public class CreatePurchaseOrderDetailCommand
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}