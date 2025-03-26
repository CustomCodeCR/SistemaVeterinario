// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.PurchaseOrder.Response;

public class PurchaseOrderResponseDto
{
    public int PurchaseOrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Supplier { get; set; }
    public string? Status { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StatePurchaseOrder { get; set; }
}