using DShop.Messages.Entities;
using System;

namespace DShop.Services.Orders.Domain
{
    public class OrderItem : IIdentifiable
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }

        protected OrderItem()
        {
        }

        public OrderItem(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
