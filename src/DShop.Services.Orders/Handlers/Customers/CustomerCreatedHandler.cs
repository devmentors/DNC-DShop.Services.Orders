using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Orders.Domain;
using DShop.Services.Orders.Messages.Events;
using DShop.Services.Orders.Repositories;
using DShop.Services.Orders.Services;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers.Customers
{
    public class CustomerCreatedHandler : IEventHandler<CustomerCreated>
    {
        private readonly IHandler _handler;
        private readonly ICustomersRepository _customersRepository;

        public CustomerCreatedHandler(IHandler handler,
            ICustomersRepository customersRepository)
        {
            _handler = handler;
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(CustomerCreated @event, ICorrelationContext context)
            => await _handler
                .Handle(async () => await _customersRepository.AddAsync(new Customer(@event.Id, 
                    @event.Email, @event.FirstName, @event.LastName, @event.Address, @event.Country))
                )
                .ExecuteAsync();
    }
}
