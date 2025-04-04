// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Payment.Response;

public class PaymentByIdResponseDto
{
    public int PaymentId { get; set; }
    public int SaleId { get; set; }
    public int Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentType { get; set; } = null!;
    public int State { get; set; }
}