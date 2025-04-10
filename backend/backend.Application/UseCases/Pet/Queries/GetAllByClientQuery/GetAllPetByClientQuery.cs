// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------s

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Pet.Response;
using MediatR;

namespace backend.Application.UseCases.Pet.Queries.GetAllByClientQuery;

public class GetAllPetByClientQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<PetResponseDto>>>
{
    public int ClientId { get; set; }
}