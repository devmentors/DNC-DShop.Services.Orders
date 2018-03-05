using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Orders;
using DShop.Messages.Events.Orders;
using DShop.Services.Orders.Services;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers
{
    public sealed class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IOrdersService _ordersService;
        private readonly IBusPublisher _busPublisher;

        public CreateOrderHandler(IOrdersService ordersService, IBusPublisher busPublisher)
        {
            _ordersService = ordersService;
            _busPublisher = busPublisher;
        }        

        public async Task HandleAsync(CreateOrder command, ICorrelationContext context)
        {
            await _ordersService.CreateAsync(command.Id, command.CustomerId, command.Number, command.ProductIds, command.TotalAmount, command.Currency);
            await _busPublisher.PublishEventAsync(new OrderCreated(command.Id, context.UserId));
        }
    }
}
