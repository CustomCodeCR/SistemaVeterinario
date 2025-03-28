// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Payment.Response;
using MediatR;

namespace backend.Application.UseCases.Payment.Queries.GetByIdQuery;

public class GetPaymentByIdQuery : IRequest<BaseResponse<PaymentByIdResponseDto>>
{
    public int PaymentId { get; set; }
}