using DShop.Common.Mongo;
using DShop.Services.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IMongoRepository<Product> _repository;

        public ProductsRepository(IMongoRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids)
            => await _repository.FindAsync(p => ids.Contains(p.Id));

        public async Task CreateAsync(Product product)
            => await _repository.CreateAsync(product);

        public async Task UpdateAsync(Product product)
            => await _repository.UpdateAsync(product);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
