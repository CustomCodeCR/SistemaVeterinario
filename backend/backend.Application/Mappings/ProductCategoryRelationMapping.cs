// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.ProductCategoryRelation.Response;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class ProductCategoryRelationMappingMappingProfile : Profile
{
    public ProductCategoryRelationMappingMappingProfile()
    {
        CreateMap<Productcategoryrelation, ProductCategoryRelationResponseDto>()
            .ForMember(x => x.RelationId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.ProductName, x => x.MapFrom(y => y.Product.Name))
            .ForMember(x => x.CategoryName, x => x.MapFrom(y => y.Category.Categoryname))
            .ForMember(x => x.StateRelation, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACIVO"))
            .ReverseMap();

        CreateMap<Productcategoryrelation, ProductCategoryRelationByIdResponseDto>()
            .ForMember(x => x.RelationId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        //CreateMap<>();
    }
}