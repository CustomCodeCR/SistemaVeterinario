// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Appointment.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.Appointment.Queries.GetAllByMedicQuery;

public class GetAllAppointmentByMedicHandler : IRequestHandler<GetAllAppointmentByMedicQuery, BaseResponse<IEnumerable<AppointmentResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllAppointmentByMedicHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<AppointmentResponseDto>>> Handle(GetAllAppointmentByMedicQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<AppointmentResponseDto>>();

        try
        {
            var appointments = _unitOfWork.Appointment.GetAllQueryable()
                .Include(x => x.Pet)
                .Include(x => x.Medic)
                .Include(x => x.Medic.User)
                .AsQueryable(); ;

            appointments = appointments.Where(x => x.Medicid.Equals(request.MedicId));

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        appointments = appointments.Where(x => x.Pet.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                appointments = appointments.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                appointments = appointments.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                         x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, appointments)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await appointments.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<AppointmentResponseDto>>(items);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}