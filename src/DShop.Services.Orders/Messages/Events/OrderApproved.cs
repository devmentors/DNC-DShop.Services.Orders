using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Orders.Messages.Events
{
    public class OrderApproved : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public OrderApproved(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}