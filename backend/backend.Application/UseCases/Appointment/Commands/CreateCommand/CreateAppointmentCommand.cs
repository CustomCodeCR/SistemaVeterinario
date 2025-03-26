// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Appointment.Commands.CreateCommand;

public class CreateAppointmentCommand : IRequest<BaseResponse<bool>>
{
    public DateTime AppointmentDate { get; set; }
    public string? Reason { get; set; }
    public int PetId { get; set; }
    public int MedicId { get; set; }
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
    public IEnumerable<CreateAppointmentDetailCommand> AppointmentDetail { get; set; } = null!;
}

public class CreateAppointmentDetailCommand
{
    public string Diagnosis { get; set; } = null!;
    public string Treatment { get; set; } = null!;
    public string Observations { get; set; } = null!;
}