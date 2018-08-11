using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using DShop.Common.Metrics;
using DShop.Common.Mvc;

namespace DShop.Services.Orders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseLockbox()
                .UseAppMetrics();
    }
}