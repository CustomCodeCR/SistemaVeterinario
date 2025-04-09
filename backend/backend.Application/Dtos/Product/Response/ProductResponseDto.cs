// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Product.Response;

public class ProductResponseDto
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public int Price { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateProduct { get; set; }
}