using DShop.Common.Messages;
using Newtonsoft.Json;
using System;

namespace DShop.Services.Orders.Messages.Commands
{
    public class CreateOrderDiscount : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public CreateOrderDiscount(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
