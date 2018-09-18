using DShop.Services.Orders.Dto;
using RestEase;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Services
{
    public interface ICustomersService
    {
        [AllowAnyStatusCode]
        [Get("carts/{id}")]
        Task<CartDto> GetCartAsync([Path] Guid id);
    }
}
