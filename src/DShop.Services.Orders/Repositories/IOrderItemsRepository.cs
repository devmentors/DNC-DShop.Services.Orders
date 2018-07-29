using DShop.Services.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Repositories
{
    public interface IOrderItemsRepository
    {
        Task<OrderItem> GetAsync(Guid id);
        Task<IEnumerable<OrderItem>> GetAsync(IEnumerable<Guid> ids);
        Task CreateAsync(OrderItem product);
        Task UpdateAsync(OrderItem product);
        Task DeleteAsync(Guid id);
    }
}
