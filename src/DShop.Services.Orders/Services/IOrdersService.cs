using DShop.Services.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Services
{
    public interface IOrdersService
    {
        Task<OrderDto> GetDtoAsync(Guid id);

        Task CreateAsync(Guid id, Guid customerId, long number, IEnumerable<Guid> productIds, decimal totalAmount, string currency);
        Task CompleteAsync(Guid id);
        Task CancelAsync(Guid id);
    }
}
