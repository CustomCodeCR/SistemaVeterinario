// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Medic.Response;
using MediatR;

namespace backend.Application.UseCases.Medic.Queries.GetByIdQuery;

public class GetMedicByIdQuery : IRequest<BaseResponse<MedicByIdResponseDto>>
{
    public int MedicId { get; set; }
}