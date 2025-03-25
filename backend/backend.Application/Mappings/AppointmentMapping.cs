// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.Appointment.Response;
using backend.Application.UseCases.Appointment.Commands.CreateCommand;
//using backend.Application.UseCases.Appointment.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class AppointmentMapping : Profile
{
    public AppointmentMapping()
    {
        /*CreateMap<Appointment, AppointmentResponseDto>()
            .ForMember(x => x.AppointmentId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.FirstName, x => x.MapFrom(y => y.User.Firstname))
            .ForMember(x => x.LastName, x => x.MapFrom(y => y.User.Lastname))
            .ForMember(x => x.StateAppointment, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Appointment, AppointmentByIdResponseDto>()
            .ForMember(x => x.AppointmentId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.UserId, x => x.MapFrom(y => y.Userid))
            .ReverseMap();*/

        CreateMap<CreateAppointmentCommand, Appointment>();

        //CreateMap<UpdateAppointmentCommand, Appointment>();
    }
}