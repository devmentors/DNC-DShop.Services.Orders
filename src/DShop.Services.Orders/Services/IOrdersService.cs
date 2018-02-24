using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Services
{
    public interface IOrdersService
    {
        Task CreateAsync(Guid id, Guid customerId, long number, IEnumerable<Guid> productIds, decimal totalAmount);
    }
}
