using DShop.Messages.Entities;
using System;

namespace DShop.Services.Orders.Domain
{
    public class OrderItem : IIdentifiable
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        protected OrderItem()
        {
        }

        public OrderItem(Guid id, string name, int quantity, decimal unitPrice)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
