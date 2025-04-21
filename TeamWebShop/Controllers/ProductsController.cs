using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using TeamWebShop.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeamWebShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ShopContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            var products = _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .OrderBy(p => p.ProductName)
            .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            return View(await products.ToListAsync());
        }
         
         public IActionResult Create()
         {
             ViewBag.Brands = new SelectList(_context.Brands, "Id", "BrandName");
             ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName");
             return View();
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("ProductName,Description,Price,BrandId,CategoryId,Color,ProductModel")] Product product, List<IFormFile> productImages)
         {
             if (product.BrandId > 0)
                 product.Brand = await _context.Brands.FindAsync(product.BrandId) ?? throw new NullReferenceException();
    
             if (product.CategoryId > 0)
                 product.Category = await _context.Categories.FindAsync(product.CategoryId) ?? throw new NullReferenceException();
    
             ModelState.Remove("Brand");
             ModelState.Remove("Category");
             ModelState.Remove("ProductImages");
             
             foreach (var state in ModelState)
             {
                 if (state.Value.Errors.Count > 0)
                 {
                     Console.WriteLine($"Поле: {state.Key}, Ошибки: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
                 }
             }
             
             if (ModelState.IsValid)
             {
                 _context.Add(product);
                 await _context.SaveChangesAsync();
                 
                 if (productImages != null && productImages.Count > 0)
                 {
                     foreach (var imageFile in productImages)
                     {
                         if (imageFile.Length > 0)
                         {
                             using (var memoryStream = new MemoryStream())
                             {
                                 await imageFile.CopyToAsync(memoryStream);
                                 var productImage = new ProductImage
                                 {
                                     ProductId = product.Id,
                                     ImageData = memoryStream.ToArray()
                                 };
                                 _context.Images.Add(productImage);
                             }
                         }
                     }
                     await _context.SaveChangesAsync();
                 }
                 return RedirectToAction(nameof(Index));
             }

             ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId);
             ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
             return View(product);
         }
         
         [HttpGet]
         public IActionResult GetImage(int id)
         {
             var image = _context.Images.FirstOrDefault(i => i.ProductId == id); // Используем ProductId
             if (image == null || image.ImageData == null)
             {
                 return NotFound();
             }

             return File(image.ImageData, "image/jpeg");
         }
         
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Delete(int id)
         {
             var product = await _context.Products
             .Include(p => p.ProductImages)
             .FirstOrDefaultAsync(p => p.Id == id);

             if (product == null)
             {
                 return NotFound();
             }

             if (product.ProductImages != null && product.ProductImages.Any())
             {
                 _context.Images.RemoveRange(product.ProductImages);
             }

             _context.Products.Remove(product);
             await _context.SaveChangesAsync();

             return RedirectToAction(nameof(Index));
         }
    }
}