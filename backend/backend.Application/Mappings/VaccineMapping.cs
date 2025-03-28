// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Select.Response;
using backend.Application.Dtos.Vaccine.Response;
using backend.Application.UseCases.Vaccine.Commands.CreateCommand;
using backend.Application.UseCases.Vaccine.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class VaccineMapping : Profile
{
    public VaccineMapping()
    {
        CreateMap<Vaccine, VaccineResponseDto>()
            .ForMember(x => x.VaccineId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.VaccineName, x => x.MapFrom(y => y.Vaccinename))
            .ForMember(x => x.StateVaccine, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Vaccine, VaccineByIdResponseDto>()
            .ForMember(x => x.VaccineId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.VaccineName, x => x.MapFrom(y => y.Vaccinename))
            .ReverseMap();

        CreateMap<CreateVaccineCommand, Vaccine>();

        CreateMap<UpdateVaccineCommand, Vaccine>();
    }
}