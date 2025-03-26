// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Client.Commands.DeleteCommand;

public class DeleteClientCommand : IRequest<BaseResponse<bool>>
{
    public int ClientId { get; set; }
    public int AuditDeleteUser { get; set; }
}