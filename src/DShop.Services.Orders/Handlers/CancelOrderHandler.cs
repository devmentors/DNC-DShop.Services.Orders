using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Orders;
using DShop.Messages.Events.Orders;
using DShop.Services.Orders.Services;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers
{
    public sealed class CancelOrderHandler : ICommandHandler<CancelOrder>
    {
        private readonly IOrdersService _ordersService;
        private readonly IBusPublisher _busPublisher;

        public CancelOrderHandler(IOrdersService ordersService, IBusPublisher busPublisher)
        {
            _ordersService = ordersService;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CancelOrder command, ICorrelationContext context)
        {
            await _ordersService.CancelAsync(command.Id);
            await _busPublisher.PublishEventAsync(new OrderCanceled(command.Id), context);
        }
    }
}
