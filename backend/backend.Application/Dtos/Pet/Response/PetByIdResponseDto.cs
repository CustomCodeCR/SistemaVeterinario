// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Pet.Response;

public class PetByIdResponseDto
{
    public int PetId { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Breed { get; set; }
    public int Age { get; set; }
    public int ClientId { get; set; }
    public int State { get; set; }
}