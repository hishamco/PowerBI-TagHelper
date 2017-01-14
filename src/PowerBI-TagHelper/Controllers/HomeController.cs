using Microsoft.AspNetCore.Mvc;

namespace PowerBI_TagHelper.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index() => View();
    }
}
