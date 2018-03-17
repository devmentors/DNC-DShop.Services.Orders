using DShop.Services.Orders.Domain;
using DShop.Services.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids)
            => await _productsRepository.GetAsync(ids);

        public async Task CreateAsync(Guid id, string name, decimal price)
            => await _productsRepository.CreateAsync(new Product(id, name, price));

        public async Task UpdateAsync(Guid id, string name, decimal price)
            => await _productsRepository.UpdateAsync(new Product(id, name, price));

        public async Task DeleteAsync(Guid id)
            => await _productsRepository.DeleteAsync(id);
    }
}
