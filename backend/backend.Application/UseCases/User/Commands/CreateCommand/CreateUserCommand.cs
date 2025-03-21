// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.User.Commands.CreateCommand;

public class CreateUserCommand : IRequest<BaseResponse<bool>>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserType { get; set; } = null!;
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
}