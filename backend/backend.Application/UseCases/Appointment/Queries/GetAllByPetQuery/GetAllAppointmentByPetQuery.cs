// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Appointment.Response;
using MediatR;

namespace backend.Application.UseCases.Appointment.Queries.GetAllByPetQuery;

public class GetAllAppointmentByPetQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<AppointmentResponseDto>>>
{
    public int PetId { get; set; }
}