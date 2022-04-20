using Pay.Data.Base;
using Pay.Data.Connection;
using Dapper;
using Pay.Core.Abstractions.Repositories;
using Pay.Core.Models;

namespace Pay.Data.Repositories
{
    public class ItemRepository : Repository, IItemRepository
    {
        public ItemRepository(SqlServerOptions options) 
            : base(options)
        {

        }

        public async Task InsertAsync(Item model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("insert into Items (Id, Name, CreatedAt) values (@Id, @Name, @CreatedAt)", model);
            }
        }

        public async Task<Item> SelectAsync(Guid id)
        {
            using (Connection)
            {
                return await Connection.QueryFirstOrDefaultAsync<Item>("select * from Items where Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(Item model)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync("update Items set Name = @Name where Id = @Id", model); 
            }
        }
    }
}