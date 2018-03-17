using DShop.Services.Orders.Domain;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Repositories
{
    public interface IOrdersRepository
    {
        Task<Order> GetAsync(Guid id);
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
}
