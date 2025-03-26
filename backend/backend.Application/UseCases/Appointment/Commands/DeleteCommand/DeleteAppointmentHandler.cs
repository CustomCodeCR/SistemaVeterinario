// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using Dapper;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.Appointment.Commands.DeleteCommand;

public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existAppointment = await _unitOfWork.Appointment.GetByIdAsync(request.AppointmentId);

            if (existAppointment is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var appointmentDetails = await _unitOfWork.AppointmentDetail.GetAppointmentDetailByAppointmentId(request.AppointmentId);

            foreach (var detail in appointmentDetails)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("PAuditDeleteUser", request.AuditDeleteUser);
                detailParams.Add("PAppointmentDetailId", detail.Id);

                await _unitOfWork.AppointmentDetail.ExecAsync(SP.SpDeleteAppointmentDetail, detailParams);
            }

            var appointmentParams = new DynamicParameters();
            appointmentParams.Add("PAuditDeleteUser", request.AuditDeleteUser);
            appointmentParams.Add("PAppointmentId", request.AppointmentId);

            var result = await _unitOfWork.Appointment.ExecAsync(SP.SpDeleteAppointment, appointmentParams);

            response.IsSuccess = result;
            response.Message = result ? ReplyMessage.MESSAGE_DELETE : ReplyMessage.MESSAGE_FAILED;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}