// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Client.Commands.CreateCommand;

public class CreateClientCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; set; }
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
}