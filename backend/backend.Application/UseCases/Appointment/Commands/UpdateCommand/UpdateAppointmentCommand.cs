// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Appointment.Commands.UpdateCommand;

public class UpdateAppointmentCommand : IRequest<BaseResponse<bool>>
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Reason { get; set; }
    public int PetId { get; set; }
    public int MedicId { get; set; }
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
    public IEnumerable<UpdateAppointmentDetailCommand> AppointmentDetail { get; set; } = null!;
}

public class UpdateAppointmentDetailCommand
{
    public int AppointmentDetailId { get; set; }
    public string Diagnosis { get; set; } = null!;
    public string Treatment { get; set; } = null!;
    public string Observations { get; set; } = null!;
}