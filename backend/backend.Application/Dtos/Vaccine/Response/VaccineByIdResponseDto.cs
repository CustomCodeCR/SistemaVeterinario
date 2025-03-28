// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Vaccine.Response;

public class VaccineByIdResponseDto
{
    public int VaccineId { get; set; }
    public string? VaccineName { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public int State { get; set; }
}