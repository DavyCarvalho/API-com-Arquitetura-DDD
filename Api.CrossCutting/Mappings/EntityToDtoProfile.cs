using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDto, UserEntity>() // converte do 1º para o 2º
                .ReverseMap(); // permite que o processo seja feito ao contrario
            CreateMap<UserDtoCreateResult, UserEntity>()
                .ReverseMap();
            CreateMap<UserDtoUpdateResult, UserEntity>()
                .ReverseMap();
        }
    }
}
