using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Orders.Messages.Events
{
    public class ApproveOrderRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ApproveOrderRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}