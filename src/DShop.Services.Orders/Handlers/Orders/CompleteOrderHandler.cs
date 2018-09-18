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
        private readonly IHandler _handler;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBusPublisher _busPublisher;

        public CompleteOrderHandler(IHandler handler, 
            IOrdersRepository ordersRepository, 
            IBusPublisher busPublisher)
        {
            _handler = handler;
            _ordersRepository = ordersRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CompleteOrder command, ICorrelationContext context)
            => await _handler.Handle(async () =>
            {
                var order = await _ordersRepository.GetAsync(command.Id);
                if (order == null || order.CustomerId != command.CustomerId)
                {
                    throw new DShopException("order_not_found",
                        $"Order with id: '{command.Id}' was not found for customer with id: '{command.CustomerId}'.");
                }
                order.Complete();
                await _ordersRepository.UpdateAsync(order);
            })
            .OnSuccess(async () =>  await _busPublisher.PublishAsync(
                new OrderCompleted(command.Id, command.CustomerId), context)
            )
            .OnCustomError(async ex => await _busPublisher.PublishAsync(
                new CompleteOrderRejected(command.Id, ex.Message, ex.Code), context)
            )
            .OnError(async ex => await _busPublisher.PublishAsync(
                new CompleteOrderRejected(command.Id, ex.Message, "complete_order_failed"), context)
            )
            .ExecuteAsync();
    }
}
