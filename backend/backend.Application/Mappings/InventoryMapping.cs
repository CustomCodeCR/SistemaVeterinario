// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Select.Response;
using backend.Application.Dtos.Inventory.Response;
using backend.Application.UseCases.Inventory.Commands.CreateCommand;
using backend.Application.UseCases.Inventory.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class InventoryMapping : Profile
{
    public InventoryMapping()
    {
        CreateMap<Inventory, InventoryResponseDto>()
            .ForMember(x => x.InventoryId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Product, x => x.MapFrom(y => y.Product.Name))
            .ForMember(x => x.UpdateDate, x => x.MapFrom(y => y.Updatedate))
            .ForMember(x => x.StateInventory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Inventory, InventoryByIdResponseDto>()
            .ForMember(x => x.InventoryId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Productid))
            .ForMember(x => x.UpdateDate, x => x.MapFrom(y => y.Updatedate))
            .ReverseMap();

        CreateMap<CreateInventoryCommand, Inventory>();

        CreateMap<UpdateInventoryCommand, Inventory>();
    }
}