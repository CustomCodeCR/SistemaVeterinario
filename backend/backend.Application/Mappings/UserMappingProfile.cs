using AutoMapper;
using backend.Application.Dtos.User.Response;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserResponseDto>()
            .ForMember(x => x.UsertId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateUser, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACIVO"))
            .ReverseMap();

        CreateMap<User, UserByIdResponseDto>()
            .ForMember(x => x.UsertId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        //CreateMap<>();
    }
}