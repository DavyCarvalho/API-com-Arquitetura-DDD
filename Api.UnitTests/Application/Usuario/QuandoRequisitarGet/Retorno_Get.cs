using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.UnitTests.Application.Usuario.QuandoRequisitarGet
{
    public class Retorno_Get
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possível Realizar o Get.")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                 new UserDto
                 {
                     Id = Guid.NewGuid(),
                     Name = nome,
                     Email = email,
                     CreateAt = DateTime.UtcNow
                 }
            );

            _controller = new UsersController(serviceMock.Object);
            var result = await _controller.Get(Guid.NewGuid());
            (result is OkObjectResult).Should().BeTrue();
            var resultValue = ((OkObjectResult)result).Value as UserDto;
            resultValue.Should().NotBeNull();
            resultValue.Name.Should().Be(nome);
            resultValue.Email.Should().Be(email);
        }
    }
}
