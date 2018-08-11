using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DShop.Common.Dispatchers;
using DShop.Common.Mongo;
using DShop.Common.Mvc;
using DShop.Common.RabbitMq;
using DShop.Common.RestEase;
using DShop.Services.Orders.Messages.Commands;
using DShop.Services.Orders.Messages.Events;
using DShop.Services.Orders.Domain;
using DShop.Services.Orders.ServiceForwarders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DShop.Services.Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddRabbitMq();
            builder.AddMongoDB();
            builder.AddMongoDBRepository<Order>("Orders");
            builder.AddMongoDBRepository<OrderItem>("OrderItems");
            builder.AddMongoDBRepository<Customer>("Customers");
            builder.RegisterServiceForwarder<IProductsApi>("products-service");
            builder.RegisterServiceForwarder<ICartsApi>("customers-service");
            builder.RegisterServiceForwarder<ICustomersApi>("customers-service");

            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseErrorHandler();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateOrder>()
                .SubscribeCommand<CancelOrder>()
                .SubscribeCommand<CompleteOrder>()
                .SubscribeEvent<CustomerCreated>();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
