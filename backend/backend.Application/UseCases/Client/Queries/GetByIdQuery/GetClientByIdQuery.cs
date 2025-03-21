// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Client.Response;
using MediatR;

namespace backend.Application.UseCases.Client.Queries.GetByIdQuery;

public class GetClientByIdQuery : IRequest<BaseResponse<ClientByIdResponseDto>>
{
    public int ClientId { get; set; }
}