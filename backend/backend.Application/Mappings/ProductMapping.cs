// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.Product.Response;
using backend.Application.UseCases.Product.Commands.CreateCommand;
using backend.Application.UseCases.Product.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Product, ProductByIdResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateProductCommand, Product>();

        CreateMap<UpdateProductCommand, Product>();
    }
}