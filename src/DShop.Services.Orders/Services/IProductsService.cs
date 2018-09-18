using System;
using System.Threading.Tasks;
using DShop.Services.Orders.Dto;
using RestEase;

namespace DShop.Services.Orders.Services
{
    public interface IProductsService
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<OrderItemDto> GetAsync([Path] Guid id);
    }
}