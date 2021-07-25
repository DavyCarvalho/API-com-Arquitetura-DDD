using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataSet; /// CRIAR CONTEXTO ????

        public UserImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<UserEntity>();
        }
        public async Task<UserEntity> FindByLogin(string email)
        {
            return await _dataSet.FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}
