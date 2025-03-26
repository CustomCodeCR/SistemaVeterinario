// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Appointment.Response;
using MediatR;

namespace backend.Application.UseCases.Appointment.Queries.GetByIdQuery;

public class GetAppointmentByIdQuery : IRequest<BaseResponse<AppointmentByIdResponseDto>>
{
    public int AppointmentId { get; set; }
}