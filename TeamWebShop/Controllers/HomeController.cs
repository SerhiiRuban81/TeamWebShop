using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            IQueryable<Product> productsQuery = _context.Product
              .Include(c => c.Category)
              .Include(b => b.Brand)
              .Include(i => i.ProductImages);


            if (!string.IsNullOrWhiteSpace(search))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(search));
            }

            var productsList = await productsQuery.ToListAsync();

            IndexVM indexVM = new IndexVM
            {
                Product = mapper.Map<IEnumerable<ProductDTO>>(productsList),
                Search = search
            };

            return View(indexVM);
        }
        //public async Task<IActionResult> Index(string? search)
        //{
        //    var products = await _context.Products
        //      .Include(c => c.Category)
        //      .Include(b => b.Brand)
        //      .Include(i => i.ProductImages)
        //      .ToListAsync();
        //    return View(products);
        //}
        //public async Task<IActionResult> IndexVM(string? search)
        //{
        //    IQueryable<ProductDTO> products = _context.Products.Where(t => t.IsDeleted == false);
        //    if (search != null)
        //    {
        //        products = products.Where(r => r.ProductName.Contains(search));
        //    }
        //    var productsList = products.ToList();

        //    IndexVM indexVM = new IndexVM()
        //    {
        //        Products = mapper.Map<IEnumerable<ProductDTO>>(productsList),

        //        Search = search,

        //    };
        //    return View(indexVM);
        //}
    }
}
