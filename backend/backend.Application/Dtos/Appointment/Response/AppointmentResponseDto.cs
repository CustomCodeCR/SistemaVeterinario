// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Appointment.Response;

public class AppointmentResponseDto
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Reason { get; set; }
    public string? Pet { get; set; }
    public string? Medic { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateAppointment { get; set; }
}