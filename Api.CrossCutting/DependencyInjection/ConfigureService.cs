using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Api.Service.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService> ();
            serviceCollection.AddTransient<ILoginService, LoginService> ();
            //Toda vez que eu for injetar a ILoginService eu vou fazer o uso de uma instancia da LoginService
        }
    }
}
