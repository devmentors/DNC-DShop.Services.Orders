using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Services.Orders.Dtos;
using DShop.Services.Orders.Queries;
using DShop.Services.Orders.Repositories;

namespace DShop.Services.Orders.Handlers.Orders
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IOrdersRepository _ordersRepository;
        
        public GetOrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<OrderDto> HandleAsync(GetOrder query)
        {
            var order = await _ordersRepository.GetAsync(query.Id);

            return order == null ? null : new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ProductIds = order.ProductIds,
                Number = order.Number,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Currency = order.Currency
            };
        }
    }
}