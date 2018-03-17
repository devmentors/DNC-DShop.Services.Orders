using System;
using System.Threading.Tasks;
using DShop.Services.Orders.Dtos;
using RestEase;

namespace DShop.Services.Orders.ServiceForwarders
{
    public interface IProductsApi
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<ProductDto> GetAsync([Path] Guid id);
    }
}