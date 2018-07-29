using DShop.Services.Orders.Dtos;
using RestEase;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Orders.ServiceForwarders
{
    public interface ICustomersApi
    {
        [AllowAnyStatusCode]
        [Get("customers/{id}")]
        Task<CustomerDto> GetAsync([Path] Guid id);
    }
}
