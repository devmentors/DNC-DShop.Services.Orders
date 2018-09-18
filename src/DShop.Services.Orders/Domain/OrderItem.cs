using System;
using DShop.Common.Types;

namespace DShop.Services.Orders.Domain
{
    public class OrderItem : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
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
