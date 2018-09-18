using DShop.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DShop.Services.Orders.Domain
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; private set; }
        public IEnumerable<OrderItem> Items { get; private set; } = new HashSet<OrderItem>();
        public decimal TotalAmount { get; private set; }
        public string Currency { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Guid id, Guid customerId, IEnumerable<OrderItem> items, string currency)
            : base(id)
        {
            if (items == null || !items.Any())
            {
                throw new DShopException("cannot_create_empty_order", 
                    $"Cannot create an order for an empty cart for customer with id: '{customerId}'.");
            }
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new DShopException("invalid_currency", 
                    $"Cannot create an order with invalid currency for customer with id: '{customerId}'.");
            }
            CustomerId = customerId;
            Items = items;
            Currency = currency;
            Status = OrderStatus.Created;
            TotalAmount = Items.Sum(i => i.TotalPrice);
        }
        public void Complete()
        {
            if (!Items.Any())
            {
                throw new DShopException("cannot_complete_empty_order",
                    $"Cannot complete an empty order with id: '{Id}'.");
            }
            if (Status == OrderStatus.Canceled)
            {
                throw new DShopException("cannot_complete_canceled_order",
                    $"Cannot complete canceled order with id: '{Id}'.");
            }
            if (Status == OrderStatus.Completed)
            {
                throw new DShopException("cannot_complete_completed_order",
                    $"Cannot complete already completed order with id: '{Id}'.");
            }

            Status = OrderStatus.Completed;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Canceled)
            {
                throw new DShopException("cannot_cancel_canceled_order",
                    $"Cannot cancel already canceled order with id: '{Id}'");
            }
            if (Status == OrderStatus.Completed)
            {
                throw new DShopException("cannot_cancel_completed_order",
                    $"Cannot cancel completed order with id: '{Id}'");
            }

            Status = OrderStatus.Canceled;
        }

        public enum OrderStatus : byte
        {
            Created = 0,
            Completed = 1,
            Canceled = 2,
        }
    }
}
