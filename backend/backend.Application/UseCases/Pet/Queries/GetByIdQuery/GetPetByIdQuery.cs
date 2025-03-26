// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Pet.Response;
using MediatR;

namespace backend.Application.UseCases.Pet.Queries.GetByIdQuery;

public class GetPetByIdQuery : IRequest<BaseResponse<PetByIdResponseDto>>
{
    public int PetId { get; set; }
}