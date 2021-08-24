using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Api.Tests.IntegrationTests.Usuario
{
    public class QuandoRequisitarUsuario : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        [Fact]
        public async Task E_Possivel_Realizar_Crud_Usuario()
        {
            await AdicionarToken();
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDtoCreate()
            {
                Name = _name,
                Email = _email
            };

            //Post
            var response = await PostJsonAsync(userDto, $"{hostApi}users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserDtoCreateResult>(postResult);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            registroPost.Name.Should().Be(_name);
            registroPost.Email.Should().Be(_email);
            registroPost.Id.Should().NotBe(default(Guid));

            //Get All
            response = await client.GetAsync($"{hostApi}users");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);
            listaFromJson.Should().NotBeNull();
            (listaFromJson.Count() > 0).Should().BeTrue();
            (listaFromJson.Where(r => r.Id == registroPost.Id).Count() == 1).Should().BeTrue();

            var updateUserDto = new UserDtoUpdate()
            {
                Id = registroPost.Id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            //PUT
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserDto),
                                    Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}users", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            registroAtualizado.Name.Should().NotBe(registroPost.Name);
            registroAtualizado.Email.Should().NotBe(registroPost.Email);

            //GET Id
            response = await client.GetAsync($"{hostApi}users/{registroAtualizado.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UserDto>(jsonResult);

            registroSelecionado.Should().NotBeNull();
            registroSelecionado.Name.Should().Be(registroAtualizado.Name);
            registroSelecionado.Email.Should().Be(registroAtualizado.Email);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}users/{registroSelecionado.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //GET ID depois do DELETE
            response = await client.GetAsync($"{hostApi}users/{registroSelecionado.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
