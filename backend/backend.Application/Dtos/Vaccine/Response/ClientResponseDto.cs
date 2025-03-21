// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Vaccine.Response;

public class VaccineResponseDto
{
    public int VaccineId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateVaccine { get; set; }
}