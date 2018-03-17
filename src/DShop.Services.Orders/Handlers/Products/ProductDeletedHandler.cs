using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Products;
using DShop.Services.Orders.ServiceForwarders;
using DShop.Services.Orders.Services;

namespace DShop.Services.Orders.Handlers.Products
{
    public class ProductDeletedHandler : IEventHandler<ProductDeleted>
    {
        private readonly IHandler _handler;
        private readonly IProductsService _productsService;
        private readonly IProductsApi _productsApi;

        public ProductDeletedHandler(IHandler handler, 
            IProductsService productsService,
            IProductsApi productsApi)
        {
            _handler = handler;
            _productsService = productsService;
            _productsApi = productsApi;
        }

        public async Task HandleAsync(ProductDeleted @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                var product = await _productsApi.GetAsync(@event.Id);
                await _productsService.UpdateAsync(product.Id, product.Name, product.Price);
            })
            .ExecuteAsync();
    }
}