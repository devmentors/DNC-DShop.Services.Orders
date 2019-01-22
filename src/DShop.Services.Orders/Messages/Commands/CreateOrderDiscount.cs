using DShop.Common.Messages;
using Newtonsoft.Json;
using System;

namespace DShop.Services.Orders.Messages.Commands
{
    public class CreateOrderDiscount : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public int Percentage { get; }

        [JsonConstructor]
        public CreateOrderDiscount(Guid id, Guid customerId, int percentage)
        {
            Id = id;
            CustomerId = customerId;
            Percentage = percentage;
        }
    }
}
