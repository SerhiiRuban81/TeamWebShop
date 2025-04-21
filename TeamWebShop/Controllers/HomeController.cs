using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebShop.Data;

namespace TeamWebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext _context;

        public HomeController(ShopContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
              .Include(c => c.Category)
              .Include(b => b.Brand)
              .Include(i=>i.ProductImages)
              .ToListAsync();
            return View(products);
        }
    }
}
