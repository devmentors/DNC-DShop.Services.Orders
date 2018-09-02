using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using DShop.Common.Logging;
using DShop.Common.Metrics;
using DShop.Common.Mvc;
using DShop.Common.Vault;

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
                .UseLogging()
                .UseVault()
                .UseLockbox()
                .UseAppMetrics();
    }
}