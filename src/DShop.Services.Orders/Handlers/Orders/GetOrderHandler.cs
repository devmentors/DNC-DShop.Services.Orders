using System.Linq;
using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Services.Orders.Dtos;
using DShop.Services.Orders.Queries;
using DShop.Services.Orders.Repositories;

namespace DShop.Services.Orders.Handlers.Orders
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDetailsDto>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly ICustomersRepository _customersRepository;

        public GetOrderHandler(
            IOrdersRepository ordersRepository,
            IOrderItemsRepository orderItemsRepository,
            ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _orderItemsRepository = orderItemsRepository;
            _customersRepository = customersRepository;
        }

        public async Task<OrderDetailsDto> HandleAsync(GetOrder query)
        {
            var order = await _ordersRepository.GetAsync(query.Id);

            if(order == null)
            {
                return null;
            }

            var getOrderItemsTask = _orderItemsRepository.GetAsync(order.OrderItemIds);
            var getCustomerTask = _customersRepository.GetAsync(order.CustomerId);

            await Task.WhenAll(getCustomerTask, getCustomerTask);

            var orderItems = getOrderItemsTask.Result;
            var customer = getCustomerTask.Result;

            return new OrderDetailsDto
            {
                Order = new OrderDto
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    OrderItemIds = order.OrderItemIds,
                    Number = order.Number,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status,
                    Currency = order.Currency
                },
                Customer = new CustomerDto
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Country = customer.Country
                },
                OrderItems = orderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    Name = oi.Name,
                    Price = oi.Price
                })
            };
        }
    }
}