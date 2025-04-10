// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Appointment.Response;
using MediatR;

namespace backend.Application.UseCases.Appointment.Queries.GetAllByMedicQuery;

public class GetAllAppointmentByMedicQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<AppointmentResponseDto>>>
{
    public int MedicId { get; set; }
}