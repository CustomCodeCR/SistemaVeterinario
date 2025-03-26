// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.PurchaseOrder.Commands.UpdateCommand;

public class UpdatePurchaseOrderCommand : IRequest<BaseResponse<bool>>
{
    public int PurchaseOrderId { get; set; }
    public int SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public int State { get; set; }
    public int AuditUpdateUser {  get; set; }
    public IEnumerable<UpdatePurchaseOrderDetailCommand> PurchaseOrderDetail { get; set; } = null!;
}

public class UpdatePurchaseOrderDetailCommand
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}