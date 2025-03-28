// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Commons.Select.Response;
using MediatR;

namespace backend.Application.UseCases.AppliedVaccine.Queries.GetSelectQuery;

public class GetSelectAppliedVaccineQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}