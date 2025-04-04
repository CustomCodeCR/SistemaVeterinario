// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.ProductCategoryRelation.Response;
using backend.Application.UseCases.ProductCategoryRelation.Commands.UpdateCommand;
using backend.Application.UseCases.ProductCategoryRelation.Commands.CreateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class ProductCategoryRelationMappingMappingProfile : Profile
{
    public ProductCategoryRelationMappingMappingProfile()
    {
        CreateMap<Productcategoryrelation, ProductCategoryRelationResponseDto>()
            .ForMember(x => x.ProductName, x => x.MapFrom(y => y.Product.Name))
            .ForMember(x => x.CategoryName, x => x.MapFrom(y => y.Category.Categoryname))
            .ForMember(x => x.StateRelation, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACIVO"))
            .ReverseMap();

        CreateMap<Productcategoryrelation, ProductCategoryRelationByIdResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Productid))
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Categoryid))
            .ReverseMap();

        CreateMap<CreateProductCategoryRelationCommand, Productcategoryrelation>();

        CreateMap<UpdateProductCategoryRelationCommand, Productcategoryrelation>();
    }
}