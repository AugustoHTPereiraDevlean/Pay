using Pay.Core.Base;
using Pay.Core.Models;

namespace Pay.Core.Abstractions.Repositories
{
    public interface IPlanRepository : IRepositoryBase<Plan>
    {
        Task<IEnumerable<Plan>> SelectAsync();
    }
}