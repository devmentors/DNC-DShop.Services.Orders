using DShop.Services.Orders.Dto;
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
