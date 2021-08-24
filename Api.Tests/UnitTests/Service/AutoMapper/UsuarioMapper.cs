using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Api.Tests.UnitTests.Service.AutoMapper
{
    public class UsuarioMapper : BaseTestService
    {
        [Fact(DisplayName = "É Possível Mapear os Modelos")]
        public void E_Possivel_Mapear_os_Modelos()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var listaEntity = new List<UserEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                listaEntity.Add(item);
            }

            //Model => Entity
            var entity = Mapper.Map<UserEntity>(model);
            model.Id.Should().Be(entity.Id);
            model.Name.Should().Be(entity.Name);
            model.Email.Should().Be(entity.Email);
            model.CreateAt.Should().Be(entity.CreateAt);
            model.UpdateAt.Should().Be(entity.UpdateAt);

            //Entity para Dto
            var userDto = Mapper.Map<UserDto>(entity);
            entity.Id.Should().Be(userDto.Id);
            entity.Name.Should().Be(userDto.Name);
            entity.Email.Should().Be(userDto.Email);
            entity.CreateAt.Should().Be(userDto.CreateAt);
            
            var listaDto = Mapper.Map<List<UserDto>>(listaEntity);
            Assert.True(listaDto.Count() == listaEntity.Count());
            for (int i = 0; i < listaDto.Count(); i++)
            {
                listaEntity[i].Id.Should().Be(listaDto[i].Id);
                listaEntity[i].Name.Should().Be(listaDto[i].Name);
                listaEntity[i].Email.Should().Be(listaDto[i].Email);
                listaEntity[i].CreateAt.Should().Be(listaDto[i].CreateAt);
            }

            var userDtoCreateResult = Mapper.Map<UserDtoCreateResult>(entity);
            entity.Id.Should().Be(userDtoCreateResult.Id);
            entity.Name.Should().Be(userDtoCreateResult.Name);
            entity.Email.Should().Be(userDtoCreateResult.Email);
            entity.CreateAt.Should().Be(userDtoCreateResult.CreateAt);

            var userDtoUpdateResult = Mapper.Map<UserDtoUpdateResult>(entity);
            entity.Id.Should().Be(userDtoUpdateResult.Id);
            entity.Name.Should().Be(userDtoUpdateResult.Name);
            entity.Email.Should().Be(userDtoUpdateResult.Email);
            entity.UpdateAt.Should().Be(userDtoUpdateResult.UpdateAt);

            //Dto para Model
            var userModel = Mapper.Map<UserModel>(userDto);
            userDto.Id.Should().Be(userModel.Id);
            userDto.Name.Should().Be(userModel.Name);
            userDto.Email.Should().Be(userModel.Email);
            userDto.CreateAt.Should().Be(userModel.CreateAt);

            var userDtoCreate = Mapper.Map<UserDtoCreate>(userModel);
            userModel.Name.Should().Be(userDtoCreate.Name);
            userModel.Email.Should().Be(userDtoCreate.Email);

            var userDtoUpdate = Mapper.Map<UserDtoUpdate>(userModel);
            userModel.Id.Should().Be(userDtoUpdate.Id);
            userModel.Name.Should().Be(userDtoUpdate.Name);
            userModel.Email.Should().Be(userDtoUpdate.Email);
        }
    }
}
