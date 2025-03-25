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

namespace backend.Application.UseCases.Appointment.Commands.CreateCommand;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var appointment = _mapper.Map<Entity.Appointment>(request);

            var parameters = new DynamicParameters();
            parameters.Add("PAppointmentDate", appointment.Appointmentdate, DbType.DateTime);
            parameters.Add("PReason", appointment.Reason, DbType.String);
            parameters.Add("PPetId", appointment.Petid, DbType.Int32);
            parameters.Add("PMedicId", appointment.Medicid, DbType.Int32);
            parameters.Add("PState", appointment.State, DbType.Int32);
            parameters.Add("PAuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("PAppointmentId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Appointment.ExecAsync(SP.SpCreateAppointment, parameters);

            if (!result)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
                return response;
            }

            var appointmentId = parameters.Get<int>("PAppointmentId");

            foreach (var detail in request.AppointmentDetail)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("PAppointmentId", appointmentId, DbType.Int32);
                detailParams.Add("PDiagnosis", detail.Diagnosis, DbType.String);
                detailParams.Add("PTreatment", detail.Treatment, DbType.String);
                detailParams.Add("PObservations", detail.Observations, DbType.String);
                detailParams.Add("PState", appointment.State, DbType.Int32);
                detailParams.Add("PAuditCreateUser", request.AuditCreateUser, DbType.Int32);
                detailParams.Add("PAppointmentDetailId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _unitOfWork.AppointmentDetail.ExecAsync(SP.SpCreateAppointmentDetail, detailParams);
            }

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
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