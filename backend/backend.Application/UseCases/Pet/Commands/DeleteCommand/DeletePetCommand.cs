// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Pet.Commands.DeleteCommand;

public class DeletePetCommand : IRequest<BaseResponse<bool>>
{
    public int PetId { get; set; }
    public int AuditDeleteUser { get; set; }
}