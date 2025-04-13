using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShopLibrary;
using System.Threading.Tasks;
using TeamWebShop.Data;
using TeamWebShop.Models.ViewModels.Home;

namespace TeamWebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext context;

        public HomeController(ShopContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int itemsPerPage = 10;
            IQueryable<Product> products = context.Products
                .Include(t => t.Brand)
                .Include(t => t.Category)
                .Include(t => t.ProductImages);
            // Filters if any
            int productsCount = products.Count();
            int totalPages = (int)Math.Ceiling((float)productsCount / itemsPerPage);
            products = products.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            HomeIndexVM vM = new HomeIndexVM()
            {
                CurrnetPage = page,
                TotalPages = totalPages,
                Products = await products.ToListAsync()
            };



            return View();
        }
    }
}
