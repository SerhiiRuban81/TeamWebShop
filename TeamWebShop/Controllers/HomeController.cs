using Microsoft.AspNetCore.Mvc;

namespace TeamWebShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
