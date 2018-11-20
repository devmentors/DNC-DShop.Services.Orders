using System.Linq;
using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Services.Orders.Dto;
using DShop.Services.Orders.Queries;
using DShop.Services.Orders.Repositories;

namespace DShop.Services.Orders.Handlers.Orders
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDetailsDto>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;

        public GetOrderHandler(
            IOrdersRepository ordersRepository,
            ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        public async Task<OrderDetailsDto> HandleAsync(GetOrder query)
        {
            var order = await _ordersRepository.GetAsync(query.OrderId);
            if (order == null || query.CustomerId.HasValue && query.CustomerId != order.CustomerId)
            {
                return null;
            }
            
            var customer = await _customersRepository.GetAsync(order.CustomerId);

            return new OrderDetailsDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ItemsCount = order.Items.Count(),
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString().ToLowerInvariant(),
                Currency = order.Currency,
                Customer = new CustomerDto
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Country = customer.Country
                },
                Items = order.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice
                })
            };
        }
    }
}