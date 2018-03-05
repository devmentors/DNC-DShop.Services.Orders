using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Orders;
using DShop.Messages.Events.Orders;
using DShop.Services.Orders.Services;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers
{
    public sealed class CompleteOrderHandler : ICommandHandler<CompleteOrder>
    {
        private readonly IOrdersService _ordersService;
        private readonly IBusPublisher _busPublisher;

        public CompleteOrderHandler(IOrdersService ordersService, IBusPublisher busPublisher)
        {
            _ordersService = ordersService;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CompleteOrder command, ICorrelationContext context)
        {
            await _ordersService.CompleteAsync(command.Id);
            await _busPublisher.PublishEventAsync(new OrderCompleted(command.Id, context.UserId));
        }
    }
}
