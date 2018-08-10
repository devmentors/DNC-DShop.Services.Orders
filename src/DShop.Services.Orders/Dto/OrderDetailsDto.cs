using System.Collections.Generic;

namespace DShop.Services.Orders.Dto
{
    public class OrderDetailsDto : OrderDto
    {
        public CustomerDto Customer { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}
