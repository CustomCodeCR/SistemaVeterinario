// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Sale.Response;

public class SaleResponseDto
{
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public string? Client {  get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateSale { get; set; }
}