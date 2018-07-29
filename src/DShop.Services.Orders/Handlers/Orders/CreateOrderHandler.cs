using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Orders;
using DShop.Messages.Events.Orders;
using DShop.Services.Orders.Domain;
using DShop.Services.Orders.Repositories;
using DShop.Services.Orders.ServiceForwarders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers.Orders
{
    public sealed class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IHandler _handler;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsRepository _productsRepository;
        private readonly ICartsApi _cartsApi;
        private readonly IBusPublisher _busPublisher;

        public CreateOrderHandler(
            IHandler handler,
            ICartsApi cartsApi,
            IOrdersRepository ordersRepository,
            IOrderItemsRepository productsRepository,
            IBusPublisher busPublisher)
        {
            _handler = handler;
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _cartsApi = cartsApi;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateOrder command, ICorrelationContext context)
            => await _handler.Handle(async () =>
            {
                var cart = await _cartsApi.GetAsync(command.CustomerId);
                var productIds = cart.Items.Select(i => i.ProductId);
                var products = await _productsRepository.GetAsync(productIds);
                var totalAmount = products.Sum(p =>
                {
                    var quantity = cart.Items.FirstOrDefault(i => i.ProductId == p.Id).Quantity;
                    return quantity * p.Price;
                });
                var orderNumber = new Random().Next(); 
                var order = new Order(command.Id, command.CustomerId, orderNumber, productIds, totalAmount, "USD");
                await _ordersRepository.CreateAsync(order);
                await _busPublisher.PublishEventAsync(new OrderCreated(command.Id, command.CustomerId, orderNumber), context);

            })
            .ExecuteAsync();
        
    }
}
