using System;
using Newtonsoft.Json;
using DShop.Common.Messages;

namespace DShop.Services.Orders.Messages.Events
{
    public class OrderCanceled : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public OrderCanceled(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
