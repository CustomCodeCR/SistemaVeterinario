// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Inventory.Response;

public class InventoryByIdResponseDto
{
    public int InventoryId { get; set; }
    public int Productid { get; set; }
    public int Quantity { get; set; }
    public DateTime Updatedate { get; set; }
}