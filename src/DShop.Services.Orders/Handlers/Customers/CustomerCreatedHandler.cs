using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Customers;
using DShop.Services.Orders.Domain;
using DShop.Services.Orders.Repositories;
using DShop.Services.Orders.ServiceForwarders;
using System.Threading.Tasks;

namespace DShop.Services.Orders.Handlers.Customers
{
    public class CustomerCreatedHandler : IEventHandler<CustomerCreated>
    {
        private readonly IHandler _handler;
        private readonly ICustomersRepository _customersRepository;
        private readonly ICustomersApi _customersApi;

        public CustomerCreatedHandler(
            IHandler handler,
            ICustomersRepository customersRepository,
            ICustomersApi customersApi)
        {
            _handler = handler;
            _customersRepository = customersRepository;
            _customersApi = customersApi;
        }

        public async Task HandleAsync(CustomerCreated @event, ICorrelationContext context)
            => await _handler
            .Handle(async () =>
            {
                var customer = await _customersApi.GetAsync(@event.Id);
                await _customersRepository.CreateAsync(new Customer(
                    customer.Id,
                    customer.Email,
                    customer.FirstName,
                    customer.LastName,
                    customer.Address,
                    customer.Country));
            })
            .ExecuteAsync();
    }
}
