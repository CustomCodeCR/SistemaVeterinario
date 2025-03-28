// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.ProductCategoryRelation.Response;

public class ProductCategoryRelationResponseDto
{
    public int RelationId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateRelation { get; set; }
}