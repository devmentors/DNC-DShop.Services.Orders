using DShop.Common.Types;
using DShop.Services.Orders.Dtos;
using DShop.Services.Orders.Entities;
using DShop.Services.Orders.Repositories;
using System;
using System.Collections.Generic;
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

        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
            => await _ordersRepository.GetOrderByIdAsync(id);

        public async Task CreateAsync(Guid id, Guid customerId, long number, IEnumerable<Guid> productIds, decimal totalAmount)
        {
            var order = new Order(id, customerId, number, productIds, totalAmount);
            await _ordersRepository.CreateAsync(order);
        }

        public async Task CompleteAsync(Guid id)
            => await ChangeStatusAsync(id, order => order.Complete());

        public async Task CancelAsync(Guid id)
            => await ChangeStatusAsync(id, order => order.Cancel());

        private async Task ChangeStatusAsync(Guid id, Action<Order> changeStatus)
        {
            var order = await _ordersRepository.GetByIdAsync(id);

            if (order == null)
            {
                throw new DShopException("Order not found.");
            }

            changeStatus(order);
            await _ordersRepository.UpdateAsync(order);
        }
    }
}
