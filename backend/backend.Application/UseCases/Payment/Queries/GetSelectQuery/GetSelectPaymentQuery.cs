// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Commons.Select.Response;
using MediatR;

namespace backend.Application.UseCases.Payment.Queries.GetSelectQuery;

public class GetSelectPaymentQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}