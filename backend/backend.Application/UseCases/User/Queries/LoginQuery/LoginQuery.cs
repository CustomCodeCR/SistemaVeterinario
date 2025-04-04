// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.User.Queries.LoginQuery;

public class LoginQuery : IRequest<BaseResponse<string>>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}