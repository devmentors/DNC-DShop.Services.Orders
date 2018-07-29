using DShop.Messages.Entities;
using System;

namespace DShop.Services.Orders.Domain
{
    public class Customer : IIdentifiable
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Address { get; protected set; }
        public string Country { get; protected set; }

        protected Customer()
        {
        }

        public Customer(Guid id, string email, string firstName, string lastName,
            string address, string country)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Country = country;
        }
    }
}
