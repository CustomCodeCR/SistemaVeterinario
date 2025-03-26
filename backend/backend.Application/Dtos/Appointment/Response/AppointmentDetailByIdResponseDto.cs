// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Appointment.Response;

public class AppointmentDetailByIdResponseDto
{
    public string? Diagnosis { get; set; }
    public string? Treatment { get; set; }
    public string? Observations { get; set; }
}