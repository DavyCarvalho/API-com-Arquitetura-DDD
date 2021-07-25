using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            //DOTNET quando eu for usar o IUserRepository você instacia o UserImplementation!!!

            serviceCollection.AddDbContext<MyContext> (
                options => options.UseMySql("Server=localhost;Port=3306;DataBase=dbAPI;Uid=root;Pwd=0219davy")
            );
        }

    }
}
