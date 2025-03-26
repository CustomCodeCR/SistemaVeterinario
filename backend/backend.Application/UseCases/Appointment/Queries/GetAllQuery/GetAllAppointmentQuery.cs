// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Appointment.Response;
using MediatR;

namespace backend.Application.UseCases.Appointment.Queries.GetAllQuery;

public class GetAllAppointmentQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<AppointmentResponseDto>>>
{
}