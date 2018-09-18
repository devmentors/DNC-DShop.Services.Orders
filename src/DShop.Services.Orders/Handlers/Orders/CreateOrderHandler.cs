using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Services.Orders.Messages.Commands;
using DShop.Services.Orders.Messages.Events;
using DShop.Services.Orders.Domain;
using DShop.Services.Orders.Repositories;
using DShop.Services.Orders.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers.Orders
{
    public sealed class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IHandler _handler;
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersService _customersService;
        private readonly IBusPublisher _busPublisher;

        public CreateOrderHandler(
            IHandler handler,
            ICustomersService customersService,
            IOrdersRepository ordersRepository,
            IBusPublisher busPublisher)
        {
            _handler = handler;
            _ordersRepository = ordersRepository;
            _customersService = customersService;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateOrder command, ICorrelationContext context)
            => await _handler.Handle(async () =>
            {
                if (await _ordersRepository.HasPendingOrder(command.CustomerId))
                {
                    throw new DShopException("customer_has_pending_order", 
                        $"Customer with id: '{command.CustomerId}' has already a pending order.");
                }
                var cart = await _customersService.GetCartAsync(command.CustomerId);
                var items = cart.Items.Select(i => new OrderItem(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice));
                var order = new Order(command.Id, command.CustomerId, items, "USD");
                await _ordersRepository.AddAsync(order);
            })
            .OnSuccess(async () =>  await _busPublisher.PublishAsync(
                new OrderCreated(command.Id, command.CustomerId), context)
            )
            .OnCustomError(async ex => await _busPublisher.PublishAsync(
                new CreateOrderRejected(command.Id, ex.Message, ex.Code), context)
            )
            .OnError(async ex => await _busPublisher.PublishAsync(
                new CreateOrderRejected(command.Id, ex.Message, "create_order_failed"), context)
            )
            .ExecuteAsync();
        
    }
}
