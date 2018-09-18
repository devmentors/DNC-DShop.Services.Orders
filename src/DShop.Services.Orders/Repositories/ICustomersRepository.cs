using DShop.Services.Orders.Domain;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetAsync(Guid id);
        Task AddAsync(Customer customer);
    }
}
