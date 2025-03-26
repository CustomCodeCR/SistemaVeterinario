// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Supplier.Response;

public class SupplierByIdResponseDto
{
    public int SupplierId { get; set; }
    public string? Name { get; set; }
    public string? Contact { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public int State { get; set; }
}