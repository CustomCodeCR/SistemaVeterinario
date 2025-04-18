﻿// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Payment.Commands.DeleteCommand;

public class DeletePaymentCommand : IRequest<BaseResponse<bool>>
{
    public int PaymentId { get; set; }
    public int AuditDeleteUser { get; set; }
}