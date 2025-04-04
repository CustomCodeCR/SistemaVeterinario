// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Payment.Commands.CreateCommand;

public class CreatePaymentCommand : IRequest<BaseResponse<bool>>
{
    public int SaleId { get; set; }
    public int Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentType { get; set; }
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
}