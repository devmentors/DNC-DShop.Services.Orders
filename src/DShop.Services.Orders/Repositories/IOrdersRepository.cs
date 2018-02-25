using DShop.Common.Mongo;
using DShop.Services.Orders.Dtos;
using DShop.Services.Orders.Entities;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Repositories
{
    public interface IOrdersRepository : IMongoRepository<Order>
    {
        Task<OrderDto> GetOrderByIdAsync(Guid id);
    }
}
