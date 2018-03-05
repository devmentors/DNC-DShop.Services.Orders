using DShop.Services.Orders.Dtos;
using DShop.Services.Orders.Entities;
using MongoDB.Driver;

namespace DShop.Services.Orders.Extensions
{
    public static class OrderDtoExtensions
    {
        public static IFindFluent<Order, OrderDto> AsOrderDtos(this IFindFluent<Order, Order> findFluent)
            => findFluent.Project(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                Number = o.Number,
                ProductIds = o.ProductIds,
                TotalAmount = o.TotalAmount,
                Currency = o.Currency,
                Status = o.Status
            });
    }
}
