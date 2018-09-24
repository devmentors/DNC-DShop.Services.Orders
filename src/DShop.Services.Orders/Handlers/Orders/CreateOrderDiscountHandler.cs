using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Orders.Messages.Commands;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers.Orders
{
    public sealed class CreateOrderDiscountHandler : ICommandHandler<CreateOrderDiscount>
    {
        public Task HandleAsync(CreateOrderDiscount command, ICorrelationContext context)
        {
            Console.WriteLine("I was triggered by SAGA!!!");
            return Task.CompletedTask;
        }
    }
}
