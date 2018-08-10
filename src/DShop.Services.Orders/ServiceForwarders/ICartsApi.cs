using DShop.Services.Orders.Dto;
using RestEase;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Orders.ServiceForwarders
{
    public interface ICartsApi
    {
        [AllowAnyStatusCode]
        [Get("carts/{id}")]
        Task<CartDto> GetAsync([Path] Guid id);
    }
}
