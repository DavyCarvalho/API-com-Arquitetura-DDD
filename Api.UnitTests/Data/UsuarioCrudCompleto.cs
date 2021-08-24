using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using static Api.UnitTests.Data.BaseTest;

namespace Api.UnitTests.Data
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvide;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }


        [Fact(DisplayName = "CRUD de Usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD_Usuario()
        {
            using (var context = _serviceProvide.GetService<MyContext>())
            {
                UserImplementation _repositorio = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var _registroCriado = await _repositorio.InsertAsync(_entity);
                _registroCriado.Should().NotBeNull();
                _registroCriado.Email.Should().Be(_entity.Email);
                _registroCriado.Name.Should().Be(_entity.Name);
                _registroCriado.Id.Should().NotBe(Guid.Empty);

                _entity.Name = Faker.Name.First();
                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
                _registroAtualizado.Should().NotBeNull();
                _registroAtualizado.Email.Should().Be(_entity.Email);
                _registroAtualizado.Name.Should().Be(_entity.Name);

                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                _registroSelecionado.Should().NotBeNull();
                _registroAtualizado.Email.Should().Be(_registroSelecionado.Email);
                _registroAtualizado.Name.Should().Be(_registroSelecionado.Name);

                var _todosRegistros = await _repositorio.SelectAsync();
                _todosRegistros.Should().NotBeNull();
                (_todosRegistros.Count() > 1).Should().BeTrue();

                var _removeu = await _repositorio.DeleteAsync(_registroSelecionado.Id);
                _removeu.Should().BeTrue();

                var _usuarioPadrao = await _repositorio.FindByLogin("user@example.com");
                _usuarioPadrao.Should().NotBeNull();
                _usuarioPadrao.Email.Should().Be("user@example.com");
                _usuarioPadrao.Name.Should().Be("ADMIN");
            }
        }
    }
}
