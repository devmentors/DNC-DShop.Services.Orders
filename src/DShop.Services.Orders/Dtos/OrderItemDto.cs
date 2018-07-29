using System;

namespace DShop.Services.Orders.Dtos
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
