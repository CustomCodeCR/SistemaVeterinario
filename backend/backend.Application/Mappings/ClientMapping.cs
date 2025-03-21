// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.Client.Response;
using backend.Application.Dtos.User.Response;
using backend.Application.UseCases.Client.Commands.CreateCommand;
using backend.Application.UseCases.Client.Commands.UpdateCommand;
using backend.Application.UseCases.User.Commands.CreateCommand;
using backend.Application.UseCases.User.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class ClientMapping : Profile
{
    public ClientMapping()
    {
        CreateMap<Client, ClientResponseDto>()
            .ForMember(x => x.ClientId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.FirstName, x => x.MapFrom(y => y.User.Firstname))
            .ForMember(x => x.LastName, x => x.MapFrom(y => y.User.Lastname))
            .ForMember(x => x.StateClient, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Client, ClientByIdResponseDto>()
            .ForMember(x => x.ClientId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.UserId, x => x.MapFrom(y => y.Userid))
            .ReverseMap();

        CreateMap<CreateClientCommand, Client>();

        CreateMap<UpdateClientCommand, Client>();
    }
}