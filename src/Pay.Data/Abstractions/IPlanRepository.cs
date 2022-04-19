using Pay.Data.Base;
using Pay.Models;

namespace Pay.Data.Abstractions
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<IEnumerable<Plan>> SelectAsync();
    }
}