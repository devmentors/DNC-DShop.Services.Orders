using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Services.Orders.Messages.Commands;
using DShop.Services.Orders.Messages.Events;
using DShop.Services.Orders.Repositories;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers.Orders
{
    public sealed class CompleteOrderHandler : ICommandHandler<CompleteOrder>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IOrdersRepository _ordersRepository;

        public CompleteOrderHandler(IBusPublisher busPublisher,
            IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CompleteOrder command, ICorrelationContext context)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order == null || order.CustomerId != command.CustomerId)
            {
                throw new DShopException("order_not_found",
                    $"Order with id: '{command.Id}' was not found for customer with id: '{command.CustomerId}'.");
            }

            order.Complete();
            await _ordersRepository.UpdateAsync(order);
            await _busPublisher.PublishAsync(new OrderCompleted(command.Id, command.CustomerId), context);
        }
    }
}
