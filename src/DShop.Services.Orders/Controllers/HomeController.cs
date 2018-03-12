using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Orders.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Ok("DShop Orders Service");
    }
}
