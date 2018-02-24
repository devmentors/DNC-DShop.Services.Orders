using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Orders;
using DShop.Services.Orders.Services;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers
{
    public sealed class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IOrdersService _ordersService;

        public CreateOrderHandler(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }        

        public async Task HandleAsync(CreateOrder command, ICorrelationContext context)
        {
            await _ordersService.CreateAsync(command.Id, command.CustomerId, command.Number, command.ProductIds, command.TotalAmount);
        }
    }
}
