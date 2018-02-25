using System;
using System.Threading.Tasks;
using DShop.Services.Orders.Dtos;
using DShop.Services.Orders.Services;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Orders.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
            => await _ordersService.GetOrderByIdAsync(id);
    }
}
