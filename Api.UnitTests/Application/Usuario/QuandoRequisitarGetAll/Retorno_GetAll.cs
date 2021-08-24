using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.UnitTests.Application.Usuario.QuandoRequisitarGetAll
{
    public class Retorno_GetAll
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possível Realizar o Get All.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                 new List<UserDto>
                 {
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow
                    },
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow
                    }
                 }
            );
            _controller = new UsersController(serviceMock.Object);
            var result = await _controller.GetAll();
            (result is OkObjectResult).Should().BeTrue();

            var resultValue = ((OkObjectResult)result).Value as IEnumerable<UserDto>;
            (resultValue.Count() == 2).Should().BeTrue();
        }
    }
}
