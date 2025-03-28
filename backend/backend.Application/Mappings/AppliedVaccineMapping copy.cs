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
        CreateMap<AppliedVaccine, AppliedVaccineResponseDto>()
            .ForMember(x => x.Vaccineid, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "PUESTA" : "FALTANTE"))
            .ReverseMap();

        CreateMap<AppliedVaccine, AppliedVaccineByIdResponseDto>()
            .ForMember(x => x.Vaccineid, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Petid, x => x.MapFrom(y => y.Petid))
            .ReverseMap();

        CreateMap<CreateAppliedVaccineCommand, AppliedVaccine>();

        CreateMap<UpdateAppliedVaccineCommand, AppliedVaccine>();
    }
}