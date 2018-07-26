using System;
using DShop.Common.Types;
using DShop.Services.Orders.Dtos;

namespace DShop.Services.Orders.Queries
{
    public class GetOrder : IQuery<OrderDto>
    {
        public Guid Id { get; set; }
    }
}