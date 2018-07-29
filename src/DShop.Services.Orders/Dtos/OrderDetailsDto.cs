using System.Collections.Generic;

namespace DShop.Services.Orders.Dtos
{
    public class OrderDetailsDto
    {
        public OrderDto Order { get; set; }
        public CustomerDto Customer { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
