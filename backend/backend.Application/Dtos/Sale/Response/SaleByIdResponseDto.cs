// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Sale.Response;

public class SaleByIdResponseDto
{
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public int ClientId { get; set; }
    public int State { get; set; }
    public ICollection<SaleDetailByIdResponseDto> SaleDetails { get; set; } = null!;
}