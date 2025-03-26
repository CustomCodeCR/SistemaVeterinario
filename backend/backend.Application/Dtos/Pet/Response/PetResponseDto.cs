// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Pet.Response;

public class PetResponseDto
{
    public int PetId { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Breed { get; set; }
    public int Age { get; set; }
    public string? Client { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StatePet { get; set; }
}