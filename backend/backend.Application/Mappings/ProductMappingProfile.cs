using AutoMapper;
using backend.Application.Dtos.Product.Response;
using backend.Application.UseCases.Product.Commands.CreateCommand;
using backend.Application.UseCases.Product.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACIVO"))
            .ReverseMap();

        CreateMap<Product, ProductByIdResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateProductCommand, Product>();

        CreateMap<UpdateProductCommand, Product>();
    }
}