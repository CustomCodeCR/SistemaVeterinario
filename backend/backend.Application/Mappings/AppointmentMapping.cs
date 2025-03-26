// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.Appointment.Response;
using backend.Application.UseCases.Appointment.Commands.CreateCommand;
using backend.Application.UseCases.Appointment.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class AppointmentMapping : Profile
{
    public AppointmentMapping()
    {
        CreateMap<Appointment, AppointmentResponseDto>()
            .ForMember(x => x.AppointmentId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Pet, x => x.MapFrom(y => y.Pet.Name))
            .ForMember(x => x.Medic, x => x.MapFrom(y => y.Medic.User.Firstname + " " + y.Medic.User.Lastname))
            .ForMember(x => x.StateAppointment, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Appointment, AppointmentByIdResponseDto>()
            .ForMember(x => x.AppointmentId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.PetId, x => x.MapFrom(y => y.Petid))
            .ForMember(x => x.MedicId, x => x.MapFrom(y => y.Medicid))
            .ReverseMap();

        CreateMap<Appointmentdetail, AppointmentDetailByIdResponseDto>()
            .ReverseMap();

        CreateMap<CreateAppointmentCommand, Appointment>();

        CreateMap<UpdateAppointmentCommand, Appointment>();
    }
}