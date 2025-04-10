using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using TeamWebShop.Data;
using TeamWebShop.Models.ViewModels.ProductImages;

namespace TeamWebShop.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly ShopContext _context;

        public ProductImagesController(ShopContext context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Images.Include(p => p.Product);
            return View(await shopContext.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.Images
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: ProductImages/Create
        public async Task<IActionResult> Create(int? selectedCategoryId, int? selectedBrandId)
        {
            IQueryable<Product> products = _context.Products;
            if (selectedCategoryId != null)
                products = products.Where(p => p.CategoryId == selectedCategoryId);
            if (selectedBrandId != null)
                products = products.Where(p => p.BrandId == selectedBrandId);
            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.ToListAsync();
            CreateImageVM vM = new CreateImageVM
            {
                Brands = new SelectList(brands, "Id", nameof(Brand.BrandName),
                selectedBrandId),
                Categories = new SelectList(categories, "Id",
                nameof(Category.CategoryName), selectedCategoryId),
                SelectedBrandId = selectedBrandId,
                SelectedCategoryId = selectedCategoryId,
                Products = new SelectList(products, "Id",
                nameof(Product.ProductName)),
            };
            return View(vM);
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateImageVM vM)
        {
            if (ModelState.IsValid)
            {
                foreach (IFormFile file in vM.Photos)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        ProductImage image = new ProductImage
                        {
                            ImageData = ms.ToArray(),
                            ProductId = vM.SelectedProductId
                        };
                        _context.Images.Add(image);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            IQueryable<Product> products = _context.Products;
            if (vM.SelectedCategoryId != null)
                products = products.Where(p => p.CategoryId == vM.SelectedCategoryId);
            if (vM.SelectedBrandId != null)
                products = products.Where(p => p.BrandId == vM.SelectedBrandId);
            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.ToListAsync();
            vM.Brands = new SelectList(brands, "Id", nameof(Brand.BrandName),
                vM.SelectedBrandId);
            vM.Categories = new SelectList(categories, "Id",
               nameof(Category.CategoryName), vM.SelectedCategoryId);
            vM.Products = new SelectList(products, "Id",
                nameof(Product.ProductName), vM.SelectedProductId);

            return View(vM);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.Images.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productImage.ProductId);
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageData,ProductId")] ProductImage productImage)
        {
            if (id != productImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImageExists(productImage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productImage.ProductId);
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.Images
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productImage = await _context.Images.FindAsync(id);
            if (productImage != null)
            {
                _context.Images.Remove(productImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImageExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetProducts(int? brandId, int? categoryId)
        {
            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.ToListAsync();
            IQueryable<Product> products = _context.Products;
            if (categoryId != null)
                products = products.Where(p => p.CategoryId == categoryId);
            if (brandId != null)
                products = products.Where(p => p.BrandId == brandId);
            CreateImageVM vM = new CreateImageVM
            {
                Brands = new SelectList(brands, "Id", nameof(Brand.BrandName), brandId),
                Categories = new SelectList(categories, "Id",
                nameof(Category.CategoryName), categoryId),
                SelectedBrandId = brandId,
                SelectedCategoryId = categoryId,
                Products = new SelectList(products, "Id",
                nameof(Product.ProductName)),
            };
            return PartialView("_SelectProductBlock", vM);
        }

    }
}
