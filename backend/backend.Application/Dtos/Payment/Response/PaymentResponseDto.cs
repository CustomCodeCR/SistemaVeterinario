// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Payment.Response;

public class PaymentResponseDto
{
    public int PaymentId { get; set; }
    public int Saleid { get; set; }
    public int Amount { get; set; }
    public DateTime Paymentdate { get; set; }
    public string Paymenttype { get; set; } = null!;
    public int State { get; set; }
    public DateTime AuditCreateDate { get; set; }
}