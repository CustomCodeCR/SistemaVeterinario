// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.AppliedVaccine.Response;
using MediatR;

namespace backend.Application.UseCases.AppliedVacinne.Queries.GetAllByPetQuery;

public class GetAllAppliedVaccineByPetQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<AppliedVaccineResponseDto>>>
{
    public int PetId { get; set; }
}