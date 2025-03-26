// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Pet.Commands.CreateCommand;

public class CreatePetCommand : IRequest<BaseResponse<bool>>
{
    public int ClientId { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int Age { get; set; }
    public string? Breed { get; set; }
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
}