using DShop.Services.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids);
        Task CreateAsync(Guid id, string name, decimal price);
        Task UpdateAsync(Guid id, string name, decimal price);
        Task DeleteAsync(Guid id);
    }
}
