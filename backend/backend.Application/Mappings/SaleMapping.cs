// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.Sale.Response;
using backend.Application.UseCases.Sale.Commands.CreateCommand;
using backend.Application.UseCases.Sale.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class SaleMapping : Profile
{
    public SaleMapping()
    {
        CreateMap<Sale, SaleResponseDto>()
            .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Client, x => x.MapFrom(y => y.Client.User.Firstname + " " + y.Client.User.Lastname))
            .ForMember(x => x.StateSale, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Sale, SaleByIdResponseDto>()
            .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.ClientId, x => x.MapFrom(y => y.Clientid))
            .ReverseMap();

        CreateMap<Saledetail, SaleDetailByIdResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Productid))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.Product.Name))
            .ReverseMap();

        CreateMap<CreateSaleCommand, Sale>();

        CreateMap<UpdateSaleCommand, Sale>();
    }
}