using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Orders.Messages.Commands
{
    public class ApproveOrder : ICommand
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ApproveOrder(Guid id)
        {
            Id = id;
        }
    }
}