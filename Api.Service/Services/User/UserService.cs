using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services.User
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _repository; //// INJEÇÃO DE DEPENDENCIA AQUI !!! PEDIR EXPLICAÇÃO !!!
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)  // é passado o repository no construtor, agora 
                                                                                // todas as outras funções da classe podem usar
                                                                                // os métodos da classe IRepository e IMapper 
                                                                                // quem vem da AutoMapper 🤔🤔🤔 Interessante
        {
            _repository = repository; //// INJEÇÃO DE DEPENDENCIA AQUI !!! PEDIR EXPLICAÇÃO !!!
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<UserDto>(entity) ?? new UserDto();
                                            //verifica se a entity está nula, se sim, 
                                            //ele cria um novo obj de UserDto
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var listEntity = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDto>>(listEntity);
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreate user)
        {
            var model = _mapper.Map<UserModel>(user); // Convertendo de UserDtoCreate para UserModel
            var entity = _mapper.Map<UserEntity>(model); // Convertendo de UserModel para UserEntity
            var result = await _repository.InsertAsync(entity); // Inserindo a Entidade no Bd

            return _mapper.Map<UserDtoCreateResult>(result);
        }

        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<UserDtoUpdateResult>(result);
        }
    }
}
