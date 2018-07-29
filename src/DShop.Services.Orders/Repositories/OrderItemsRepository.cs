using DShop.Common.Mongo;
using DShop.Services.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly IMongoRepository<OrderItem> _repository;

        public OrderItemsRepository(IMongoRepository<OrderItem> repository)
            => _repository = repository;

        public async Task<OrderItem> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<IEnumerable<OrderItem>> GetAsync(IEnumerable<Guid> ids)
            => await _repository.FindAsync(p => ids.Contains(p.Id));

        public async Task CreateAsync(OrderItem orderItem)
            => await _repository.CreateAsync(orderItem);

        public async Task UpdateAsync(OrderItem orderItem)
            => await _repository.UpdateAsync(orderItem);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
