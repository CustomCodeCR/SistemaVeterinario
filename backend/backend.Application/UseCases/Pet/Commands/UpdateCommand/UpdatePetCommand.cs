// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Pet.Commands.UpdateCommand;

public class UpdatePetCommand : IRequest<BaseResponse<bool>>
{
    public int PetId { get; set; }
    public int ClientId { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int Age { get; set; }
    public string? Breed { get; set; }
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}