using DShop.Messages.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; protected set; }
        public long Number { get; protected set; }
        public IEnumerable<Guid> ProductIds { get; protected set; }
        public decimal TotalAmount { get; protected set; }
    }
}
