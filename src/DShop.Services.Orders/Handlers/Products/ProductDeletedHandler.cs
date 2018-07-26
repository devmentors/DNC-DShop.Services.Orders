using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Products;
using DShop.Services.Orders.Domain;
using DShop.Services.Orders.Repositories;
using DShop.Services.Orders.ServiceForwarders;

namespace DShop.Services.Orders.Handlers.Products
{
    public class ProductDeletedHandler : IEventHandler<ProductDeleted>
    {
        private readonly IHandler _handler;
        private readonly IProductsRepository _productsRepository;
        private readonly IProductsApi _productsApi;

        public ProductDeletedHandler(IHandler handler, 
            IProductsRepository productsRepository,
            IProductsApi productsApi)
        {
            _handler = handler;
            _productsRepository = productsRepository;
            _productsApi = productsApi;
        }

        public async Task HandleAsync(ProductDeleted @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                await _productsRepository.DeleteAsync(@event.Id);
            })
            .ExecuteAsync();
    }
}