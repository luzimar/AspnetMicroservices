using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IAsyncRepository<Order> _repository;

        public OrderRepository(IAsyncRepository<Order> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            return await _repository.GetAsync(order => order.UserName == userName);
        }
    }
}
