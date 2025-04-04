// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Inventory.Response;

public class InventoryResponseDto
{
    public int InventoryId { get; set; }
    public string? Product { get; set; }
    public int Quantity { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateInventory { get; set; }
}