// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Vaccine.Response;
using MediatR;

namespace backend.Application.UseCases.Vaccine.Queries.GetByIdQuery;

public class GetVaccineByIdQuery : IRequest<BaseResponse<VaccineByIdResponseDto>>
{
    public int VaccineId { get; set; }
}