// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Pet.Response;
using MediatR;

namespace backend.Application.UseCases.Pet.Queries.GetAllQuery;

public class GetAllPetQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<PetResponseDto>>>
{
}