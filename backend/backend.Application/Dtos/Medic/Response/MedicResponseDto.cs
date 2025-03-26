// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Medic.Response;

public class MedicResponseDto
{
    public int MedicId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Specialty { get; set; }
    public string? Phone { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateMedic { get; set; }
}