using DShop.Common.Mongo;
using DShop.Services.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IMongoRepository<Customer> _customersRepository;

        public CustomersRepository(IMongoRepository<Customer> customersRepository)
            => _customersRepository = customersRepository;

        public async Task<Customer> GetAsync(Guid id)
            => await _customersRepository.GetAsync(id);

        public async Task AddAsync(Customer customer)
            => await _customersRepository.AddAsync(customer);
    }
}
