using Pay.Models;

namespace Pay.Data.Base
{
    public interface IRepository<T>
    {
        Task InsertAsync(T model);
        Task UpdateAsync(T model);
        Task<T> SelectAsync(Guid id);
    }
}