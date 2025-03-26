// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Appointment.Response;
using backend.Application.Interfaces.Services;
using backend.Application.UseCases.Appointment.Queries.GetByIdQuery;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, BaseResponse<AppointmentByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAppointmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<AppointmentByIdResponseDto>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<AppointmentByIdResponseDto>();

        try
        {
            var appointment = await _unitOfWork.Appointment.GetByIdAsync(request.AppointmentId);

            if (appointment == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var appointmentDetails = await _unitOfWork.AppointmentDetail.GetAppointmentDetailByAppointmentId(request.AppointmentId);
            appointment.Appointmentdetails = appointmentDetails.ToList();

            appointment.AuditCreateDate = appointment.AuditCreateDate.Date;

            response.IsSuccess = true;
            response.Data = _mapper.Map<AppointmentByIdResponseDto>(appointment);
            response.Message = ReplyMessage.MESSAGE_QUERY;
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