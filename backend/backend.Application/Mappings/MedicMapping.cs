// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.Medic.Response;
using backend.Application.UseCases.Medic.Commands.CreateCommand;
using backend.Application.UseCases.Medic.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class MedicMapping : Profile
{
    public MedicMapping()
    {
        CreateMap<Medic, MedicResponseDto>()
            .ForMember(x => x.MedicId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.FirstName, x => x.MapFrom(y => y.User.Firstname))
            .ForMember(x => x.LastName, x => x.MapFrom(y => y.User.Lastname))
            .ForMember(x => x.StateMedic, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Medic, MedicByIdResponseDto>()
            .ForMember(x => x.MedicId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.UserId, x => x.MapFrom(y => y.Userid))
            .ReverseMap();

        CreateMap<CreateMedicCommand, Medic>();

        CreateMap<UpdateMedicCommand, Medic>();
    }
}