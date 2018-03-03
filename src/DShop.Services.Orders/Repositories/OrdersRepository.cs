using System;
using System.Threading.Tasks;
using DShop.Common.Mongo;
using DShop.Services.Orders.Dtos;
using DShop.Services.Orders.Entities;
using DShop.Services.Orders.Extensions;
using MongoDB.Driver;

namespace DShop.Services.Orders.Repositories
{
    public class OrdersRepository : MongoRepository<Order>, IOrdersRepository
    {
        protected OrdersRepository(IMongoDatabase database) 
            : base(database, "Orders")
        {
        }

        public async Task<OrderDto> GetDtoAsync(Guid id)
            => await Collection
            .Find(p => p.Id == id)
            .AsOrderDtos()
            .SingleOrDefaultAsync();
    }
}
