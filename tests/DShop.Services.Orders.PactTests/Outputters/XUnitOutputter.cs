using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace DShop.Services.Orders.PactTests.Outputters
{
    public class XUnitOutputter : IOutput
    {
        private readonly ITestOutputHelper _output;

        public XUnitOutputter(ITestOutputHelper output)
            => _output = output;
        
        public void WriteLine(string line)
        {
            _output.WriteLine(line);
        }
    }
}