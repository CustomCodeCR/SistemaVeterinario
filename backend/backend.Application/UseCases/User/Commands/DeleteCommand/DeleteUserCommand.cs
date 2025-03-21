// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.User.Commands.DeleteCommand;

public class DeleteUserCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; set; }
    public int AuditDeleteUser { get; set; }
}