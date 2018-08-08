using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
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
        private readonly ICartsApi _cartsApi;
        private readonly IBusPublisher _busPublisher;

        public CreateOrderHandler(
            IHandler handler,
            ICartsApi cartsApi,
            IOrdersRepository ordersRepository,
            IBusPublisher busPublisher)
        {
            _handler = handler;
            _ordersRepository = ordersRepository;
            _cartsApi = cartsApi;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateOrder command, ICorrelationContext context)
            => await _handler.Handle(async () =>
            {
                var cart = await _cartsApi.GetAsync(command.CustomerId);
                if (cart == null || !cart.Items.Any())
                {
                    throw new DShopException("Cannot create an order for empty cart.");
                }
                var orderNumber = new Random().Next(); 
                var items = cart.Items.Select(i => new OrderItem(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice));
                var order = new Order(command.Id, command.CustomerId, orderNumber, items, "USD");
                await _ordersRepository.CreateAsync(order);
                await _busPublisher.PublishEventAsync(new OrderCreated(command.Id, command.CustomerId, orderNumber), context);
            })
            .ExecuteAsync();
        
    }
}
