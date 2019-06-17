using System;
using System.Collections.Generic;
using DShop.Services.Orders.PactTests.Outputters;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace DShop.Services.Orders.PactTests
{
    public class OrdersApiTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _serviceUri;
        private readonly IWebHost _webHost;
        
        public OrdersApiTests(ITestOutputHelper output)
        {
            _output = output;
            _serviceUri = "http://localhost:5005";
            
//            _webHost = WebHost.CreateDefaultBuilder()
//                .UseUrls(_)
        }

        [Fact]
        public void Pact_Should_Be_Verified()
        {
            var pactConfig = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutputter(_output)
                },
                Verbose = true
            };
            
            new PactVerifier(pactConfig)
                .ServiceProvider("Orders", _serviceUri)
                .HonoursPactWith("Discounts")
                .PactUri(@"..\..\..\..\..\..\pacts\discounts-orders.json")
                .Verify();
        }
        
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // CLEAN THE MESS
                }

                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }
}