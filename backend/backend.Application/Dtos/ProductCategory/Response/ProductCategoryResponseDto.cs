// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.ProductCategory.Response;

public class ProductCategoryResponseDto
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public int State { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public string? StateCategory { get; set; }
}