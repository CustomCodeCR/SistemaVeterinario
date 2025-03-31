// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Payment.Commands.UpdateCommand;

public class UpdatePaymentCommand : IRequest<BaseResponse<bool>>
{
    public int PaymentId { get; set; }
    public int Saleid { get; set; }
    public int Amount { get; set; }
    public DateTime Paymentdate { get; set; }
    public string Paymenttype { get; set; } = null!;
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}