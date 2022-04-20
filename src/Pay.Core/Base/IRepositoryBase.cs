namespace Pay.Core.Base
{
    public interface IRepositoryBase<T>
    {
        Task InsertAsync(T model);
        Task UpdateAsync(T model);
        Task<T> SelectAsync(Guid id);
    }
}