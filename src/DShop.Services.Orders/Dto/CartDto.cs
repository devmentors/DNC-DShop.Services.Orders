using System;
using System.Collections.Generic;

namespace DShop.Services.Orders.Dto
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public IEnumerable<CartItemDto> Items { get; set; }
    }
}
