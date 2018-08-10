using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Orders.Messages.Events
{
    public class OrderCompleted : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public OrderCompleted(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
