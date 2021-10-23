using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrderByUserName(string userName);
    }
}
