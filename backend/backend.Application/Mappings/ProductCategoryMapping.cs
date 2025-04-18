// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.ProductCategory.Response;
using backend.Application.UseCases.ProductCategory.Commands.CreateCommand;
using backend.Application.UseCases.ProductCategory.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class ProductCategoryMapping : Profile
{
    public ProductCategoryMapping()
    {
        CreateMap<Productcategory, ProductCategoryResponseDto>()
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACIVO"))
            .ReverseMap();

        CreateMap<Productcategory, ProductCategoryByIdResponseDto>()
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateProductCategoryCommand, Productcategory>();

        CreateMap<UpdateProductCategoryCommand, Productcategory>();
    }
}