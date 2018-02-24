using DShop.Common.Mongo;
using DShop.Services.Orders.Entities;

namespace DShop.Services.Orders.Repositories
{
    public interface IOrdersRepository : IMongoRepository<Order>
    {
    }
}
