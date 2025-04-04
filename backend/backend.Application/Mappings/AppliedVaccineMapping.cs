// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Select.Response;
using backend.Application.Dtos.AppliedVaccine.Response;
using backend.Application.UseCases.AppliedVaccine.Commands.CreateCommand;
using backend.Application.UseCases.AppliedVaccine.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class AppliedVaccineMapping : Profile
{
    public AppliedVaccineMapping()
    {
        CreateMap<Appliedvaccine, AppliedVaccineResponseDto>()
            .ForMember(x => x.AppliedVaccineId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Pet, x => x.MapFrom(y => y.Pet.Name))
            .ForMember(x => x.Vaccine, x => x.MapFrom(y => y.Vaccine.Vaccinename))
            .ForMember(x => x.StateAppliedVaccine, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "PUESTA" : "FALTANTE"))
            .ReverseMap();

        CreateMap<Appliedvaccine, AppliedVaccineByIdResponseDto>()
            .ForMember(x => x.AppliedVaccineId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.PetId, x => x.MapFrom(y => y.Petid))
            .ForMember(x => x.VaccineId, x => x.MapFrom(y => y.Vaccineid))
            .ReverseMap();

        CreateMap<CreateAppliedVaccineCommand, Appliedvaccine>();

        CreateMap<UpdateAppliedVaccineCommand, Appliedvaccine>();
    }
}