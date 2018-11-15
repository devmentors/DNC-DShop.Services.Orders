using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Services.Orders.Messages.Commands;
using DShop.Services.Orders.Messages.Events;
using DShop.Services.Orders.Repositories;

namespace DShop.Services.Orders.Handlers.Orders
{
    public class ApproveOrderHandler : ICommandHandler<ApproveOrder>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IOrdersRepository _ordersRepository;

        public ApproveOrderHandler(IBusPublisher busPublisher, IOrdersRepository ordersRepository)
        {
            _busPublisher = busPublisher;
            _ordersRepository = ordersRepository;
        }

        public async Task HandleAsync(ApproveOrder command, ICorrelationContext context)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order == null)
            {
                throw new DShopException("order_not_found",
                    $"Order with id: '{command.Id}' was not found.");
            }

            order.Approve();
            await _ordersRepository.UpdateAsync(order);
            await _busPublisher.PublishAsync(new OrderApproved(command.Id, order.CustomerId), context);
        }
    }
}