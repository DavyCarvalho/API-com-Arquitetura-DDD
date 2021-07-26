using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>() // CONVERTE DE MODEL PARA DTO
                .ReverseMap(); // PERMITE QUE A OPERAÇÃO SEJA FEITA AO CONTRÁRIO TAMBÉM, DTO --> MODEL
            CreateMap<UserModel, UserDtoCreate>() // CONVERTE DE MODEL PARA DTO
                .ReverseMap(); // PERMITE QUE A OPERAÇÃO SEJA FEITA AO CONTRÁRIO TAMBÉM, DTO --> MODEL
            CreateMap<UserModel, UserDtoUpdate>() // CONVERTE DE MODEL PARA DTO
                .ReverseMap(); // PERMITE QUE A OPERAÇÃO SEJA FEITA AO CONTRÁRIO TAMBÉM, DTO --> MODEL
        }
    }
}
