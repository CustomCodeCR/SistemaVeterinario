// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Vaccine.Response;

public class VaccineResponseDto
{
    public int VaccineId { get; set; }
    public string? VaccineName { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateVaccine { get; set; }
}