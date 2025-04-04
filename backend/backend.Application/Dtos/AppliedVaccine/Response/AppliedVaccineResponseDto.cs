// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.AppliedVaccine.Response;

public class AppliedVaccineResponseDto
{
    public int AppliedVaccineId { get; set; }
    public DateTime Applicationdate { get; set; }
    public string? Pet { get; set; }
    public string? Vaccine { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateAppliedVaccine { get; set; }
}