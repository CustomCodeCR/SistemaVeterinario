// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Payment.Response;
using MediatR;

namespace backend.Application.UseCases.Payment.Queries.GetAllQuery;

public class GetAllPaymentQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<PaymentResponseDto>>>
{
}