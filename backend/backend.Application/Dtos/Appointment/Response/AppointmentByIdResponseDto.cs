// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Appointment.Response;

public class AppointmentByIdResponseDto
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Reason { get; set; }
    public int PetId { get; set; }
    public int MedicId { get; set; }
    public int State { get; set; }
    public ICollection<AppointmentDetailByIdResponseDto> AppointmentDetails { get; set; } = null!;
}