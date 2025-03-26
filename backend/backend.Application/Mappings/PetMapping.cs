// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.Pet.Response;
using backend.Application.UseCases.Pet.Commands.CreateCommand;
using backend.Application.UseCases.Pet.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class PetMapping : Profile
{
    public PetMapping()
    {
        CreateMap<Pet, PetResponseDto>()
            .ForMember(x => x.PetId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Client, x => x.MapFrom(y => y.Client.User.Firstname + " " + y.Client.User.Lastname))
            .ForMember(x => x.StatePet, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Pet, PetByIdResponseDto>()
            .ForMember(x => x.PetId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.ClientId, x => x.MapFrom(y => y.Clientid))
            .ReverseMap();

        CreateMap<CreatePetCommand, Pet>();

        CreateMap<UpdatePetCommand, Pet>();
    }
}