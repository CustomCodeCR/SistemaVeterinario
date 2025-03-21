// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.User.Response;
using MediatR;

namespace backend.Application.UseCases.User.Queries.GetByIdQuery;

public class GetUserByIdQuery : IRequest<BaseResponse<UserByIdResponseDto>>
{
    public int UserId { get; set; }
}