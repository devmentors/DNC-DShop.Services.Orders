using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Messages.Commands.Orders;
using DShop.Messages.Events.Orders;
using DShop.Services.Orders.Repositories;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers.Orders
{
    public sealed class CancelOrderHandler : ICommandHandler<CancelOrder>
    {
        private readonly IHandler _handler;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBusPublisher _busPublisher;

        public CancelOrderHandler(IHandler handler, 
            IOrdersRepository ordersRepository, 
            IBusPublisher busPublisher)
        {
            _handler = handler;
            _ordersRepository = ordersRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CancelOrder command, ICorrelationContext context)
            => await _handler.Handle(async () =>
            {
                var order = await _ordersRepository.GetAsync(command.Id);
                if (order == null)
                {
                    throw new DShopException("order_not_found", "Order not found.");
                }
                order.Cancel();
                await _ordersRepository.UpdateAsync(order);
                await _busPublisher.PublishEventAsync(new OrderCanceled(command.Id, command.CustomerId), context);
            })
            .ExecuteAsync();            
    }
}
