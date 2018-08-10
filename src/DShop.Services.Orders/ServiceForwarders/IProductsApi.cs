using System;
using System.Threading.Tasks;
using DShop.Services.Orders.Dto;
using RestEase;

namespace DShop.Services.Orders.ServiceForwarders
{
    public interface IProductsApi
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<OrderItemDto> GetAsync([Path] Guid id);
    }
}