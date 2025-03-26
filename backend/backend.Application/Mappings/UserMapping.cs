// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.User.Response;
using backend.Application.UseCases.User.Commands.CreateCommand;
using backend.Application.UseCases.User.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<Appuser, UserResponseDto>()
            .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateUser, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Appuser, UserByIdResponseDto>()
            .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateUserCommand, Appuser>();

        CreateMap<UpdateUserCommand, Appuser>();
    }
}