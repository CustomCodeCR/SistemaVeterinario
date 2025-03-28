// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.AppliedVaccine.Response;
using MediatR;

namespace backend.Application.UseCases.AppliedVaccine.Queries.GetAllQuery;

public class GetAllAppliedVaccineQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<AppliedVaccineResponseDto>>>
{
}