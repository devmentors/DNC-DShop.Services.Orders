using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Orders.Messages.Events;
using Microsoft.Extensions.Logging;

namespace DShop.Services.Orders.Handlers.Discounts
{
    public class DiscountCreatedHandler : IEventHandler<DiscountCreated>
    {
        private readonly ILogger<DiscountCreatedHandler> _logger;

        public DiscountCreatedHandler(ILogger<DiscountCreatedHandler> logger)
        {
            _logger = logger;
        }
        
        public Task HandleAsync(DiscountCreated @event, ICorrelationContext context)
        {
            _logger.LogInformation($"Discount created: {@event.Id}");

            return Task.CompletedTask;
        }
    }
}