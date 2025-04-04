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
    public int SaleId { get; set; }
    public int Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentType { get; set; } = null!;
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}