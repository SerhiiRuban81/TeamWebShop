using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using TeamWebShop.Data;
using TeamWebShop.Models.DTOs.Products;
using TeamWebShop.Models.ViewModels.Products;

namespace TeamWebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext _context;
        private readonly IMapper mapper;

        public HomeController(ShopContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string? search)
        {
            var products1 = _context.Products
            .Include(c => c.Category)
            .Include(b => b.Brand)
            .Include(i => i.ProductImages)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                products1 = products1.Where(p => p.ProductName.Contains(search));
            }

            var products = await products1.ToListAsync();

            return View(products);

        }
    }
}
