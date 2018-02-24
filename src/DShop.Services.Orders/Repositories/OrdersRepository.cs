using DShop.Common.Mongo;
using DShop.Services.Orders.Entities;
using MongoDB.Driver;

namespace DShop.Services.Orders.Repositories
{
    public class OrdersRepository : MongoRepository<Order>, IOrdersRepository
    {
        protected OrdersRepository(IMongoDatabase database) 
            : base(database, "Orders")
        {
        }
    }
}
