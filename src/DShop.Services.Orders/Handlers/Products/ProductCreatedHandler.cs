using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Products;
using DShop.Services.Orders.Domain;
using DShop.Services.Orders.Repositories;
using DShop.Services.Orders.ServiceForwarders;

namespace DShop.Services.Orders.Handlers.Products
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly IHandler _handler;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IProductsApi _productsApi;

        public ProductCreatedHandler(IHandler handler, 
            IOrderItemsRepository orderItemsRepository,
            IProductsApi productsApi)
        {
            _handler = handler;
            _orderItemsRepository = orderItemsRepository;
            _productsApi = productsApi;
        }

        public async Task HandleAsync(ProductCreated @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                var product = await _productsApi.GetAsync(@event.Id);
                await _orderItemsRepository.CreateAsync(new OrderItem(product.Id, product.Name, product.Price));
            })
            .ExecuteAsync();
    }
}