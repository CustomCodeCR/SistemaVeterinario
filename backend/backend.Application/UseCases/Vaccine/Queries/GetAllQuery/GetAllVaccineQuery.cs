// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Vaccine.Response;
using MediatR;

namespace backend.Application.UseCases.Vaccine.Queries.GetAllQuery;

public class GetAllVaccineQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<VaccineResponseDto>>>
{
}