using AutoMapper;
using backend.Application.Dtos.Product.Response;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductResponseDto>()
            .ForMember(x => x.ProducttId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACIVO"))
            .ReverseMap();

        CreateMap<Product, ProductByIdResponseDto>()
            .ForMember(x => x.ProducttId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        //CreateMap<>();
    }
}