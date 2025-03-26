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
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateRelation, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACIVO"))
            .ReverseMap();

        CreateMap<Productcategoryrelation, ProductCategoryRelationByIdResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        //CreateMap<>();
    }
}