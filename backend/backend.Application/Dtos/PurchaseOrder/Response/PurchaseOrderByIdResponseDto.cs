// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.PurchaseOrder.Response;

public class PurchaseOrderByIdResponseDto
{
    public int PurchaseOrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int SupplierId { get; set; }
    public string? Status { get; set; }
    public int State { get; set; }
    public ICollection<PurchaseOrderDetailByIdResponseDto> PurchaseOrderDetails { get; set; } = null!;
}