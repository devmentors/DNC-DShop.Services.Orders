using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using DShop.Common.Mvc;

namespace DShop.Services.Orders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseLockbox()
                .Build();
    }
}