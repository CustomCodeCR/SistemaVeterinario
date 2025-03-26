// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using Dapper;
using MediatR;
using System.Data;
using WatchDog;
using Entity = backend.Domain.Entities;

namespace backend.Application.UseCases.Appointment.Commands.UpdateCommand;

public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointmentCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
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

            var appointment = _mapper.Map<Entity.Appointment>(request);
            appointment.Id = request.AppointmentId;

            var parameters = new DynamicParameters();
            parameters.Add("PAppointmentId", appointment.Id, DbType.Int32);
            parameters.Add("PAppointmentDate", appointment.Appointmentdate, DbType.DateTime);
            parameters.Add("PReason", appointment.Reason, DbType.String);
            parameters.Add("PPetId", appointment.Petid, DbType.Int32);
            parameters.Add("PMedicId", appointment.Medicid, DbType.Int32);
            parameters.Add("PState", appointment.State, DbType.Int32);
            parameters.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Appointment.ExecAsync(SP.SpUpdateAppointment, parameters);

            if (!result)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
                return response;
            }

            foreach (var detail in request.AppointmentDetail)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("PAppointmentDetailId", detail.AppointmentDetailId, DbType.Int32);
                detailParams.Add("PAppointmentId", appointment.Id, DbType.Int32);
                detailParams.Add("PDiagnosis", detail.Diagnosis, DbType.String);
                detailParams.Add("PTreatment", detail.Treatment, DbType.String);
                detailParams.Add("PObservations", detail.Observations, DbType.String);
                detailParams.Add("PState", appointment.State, DbType.Int32);
                detailParams.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

                await _unitOfWork.AppointmentDetail.ExecAsync(SP.SpUpdateAppointmentDetail, detailParams);
            }

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
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