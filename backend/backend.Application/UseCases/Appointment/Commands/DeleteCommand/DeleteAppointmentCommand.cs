using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Appointment.Commands.DeleteCommand;

public class DeleteAppointmentCommand : IRequest<BaseResponse<bool>>
{
    public int AppointmentId { get; set; }
    public int AuditDeleteUser { get; set; }
}