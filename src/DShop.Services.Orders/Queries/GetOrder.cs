using System;
using DShop.Common.Types;
using DShop.Services.Orders.Dto;

namespace DShop.Services.Orders.Queries
{
    public class GetOrder : IQuery<OrderDetailsDto>
    {
        public Guid Id { get; set; }
    }
}