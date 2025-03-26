// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.PurchaseOrder.Response;

public class PurchaseOrderDetailByIdResponseDto
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}