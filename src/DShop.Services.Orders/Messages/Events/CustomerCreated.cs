using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Orders.Messages.Events
{
    public class CustomerCreated : IEvent
    {
        public Guid Id { get;  }

        [JsonConstructor]
        public CustomerCreated(Guid id)
        {
            Id = id;
        }
    }
}