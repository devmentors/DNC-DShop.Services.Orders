using DShop.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DShop.Services.Orders.Domain
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; protected set; }
        public long Number { get; protected set; }
        public IEnumerable<OrderItem> Items { get; protected set; } = new HashSet<OrderItem>();
        public decimal TotalAmount { get; protected set; }
        public string Currency { get; protected set; }
        public OrderStatus Status { get; protected set; }

        public Order(Guid id, Guid customerId, long number, IEnumerable<OrderItem> items, string currency)
            :base(id)
        {
            CustomerId = customerId;
            Number = number;
            Items = items;
            Currency = currency;
            Status = OrderStatus.Created;
            TotalAmount = Items.Sum(i => i.TotalPrice);
        }
        public void Complete()
        {
            if (!Items.Any())
            {
                throw new DShopException("Cannot complete an empty order.");
            }
            if (Status == OrderStatus.Canceled)
            {
                throw new DShopException("Cannot complete canceled order.");
            }
            if (Status == OrderStatus.Completed)
            {
                throw new DShopException("Cannot complete already completed order.");
            }

            Status = OrderStatus.Completed;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Canceled)
            {
                throw new DShopException("Cannot cancel already canceled order.");
            }
            if (Status == OrderStatus.Completed)
            {
                throw new DShopException("Cannot cancel completed order.");
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
