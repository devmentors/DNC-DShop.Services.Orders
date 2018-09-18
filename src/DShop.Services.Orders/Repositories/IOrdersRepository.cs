using DShop.Common.Types;
using DShop.Services.Orders.Domain;
using DShop.Services.Orders.Queries;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Repositories
{
    public interface IOrdersRepository
    {
        Task<bool> HasPendingOrder(Guid customerId);
        Task<Order> GetAsync(Guid id);
        Task<PagedResult<Order>> BrowseAsync(BrowseOrders query);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
}
