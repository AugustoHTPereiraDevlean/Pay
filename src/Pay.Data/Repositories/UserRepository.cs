using Dapper;
using Pay.Core;
using Pay.Core.Abstractions.Repositories;
using Pay.Data.Base;
using Pay.Data.Connection;

namespace Pay.Data.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(SqlServerOptions options) 
            : base(options)
        {

        }

        public async Task InsertAsync(User model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into Users (Id, Name) values (@Id, @Name)", model);
            }
        }

        public async Task<User> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<User>("select * from Users where Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(User model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update Users set Name = @Name where Id = @Id", model); 
            }
        }
    }
}