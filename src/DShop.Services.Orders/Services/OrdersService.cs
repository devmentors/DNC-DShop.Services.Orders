using DShop.Services.Orders.Entities;
using DShop.Services.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task CreateAsync(Guid id, Guid customerId, long number, IEnumerable<Guid> productIds, decimal totalAmount)
        {
            var order = new Order(id, customerId, number, productIds, totalAmount);
            await _ordersRepository.CreateAsync(order);
        }
    }
}
