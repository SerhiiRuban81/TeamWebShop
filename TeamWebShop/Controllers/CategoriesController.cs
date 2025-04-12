using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using TeamWebShop.Data;
using TeamWebShop.Models.ViewModels.Categories;
using TeamWebShop.Models.ViewModels.Products;

namespace TeamWebShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ShopContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c=>c.ChildCategories)
                .ToListAsync();

            return View(category);
        }

        // GET: Categories/Create
        //[Authorize(Policy = "managerPolicy")]
        public async Task<IActionResult> Create()
        {
            var category = await _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync();
            CreateCategoryVM vM = new CreateCategoryVM()
            {
                ParentCategories = new SelectList(category, "Id", "CategoryName")
            };
            return View(vM);
        }

        // POST: Categories/Create

       // [Authorize(Policy = "managerPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM vM, IList<int?> parentCategoryId)
        {
            if (ModelState.IsValid)
            {
                parentCategoryId = parentCategoryId.Where(t => t.HasValue).ToList();
                Category category = _mapper.Map<Category>(vM.CategoryDTO);
                if (parentCategoryId.Count() > 0)
                    category.ParentCategoryId = parentCategoryId[parentCategoryId.Count() - 1];
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vM.ParentCategories = new SelectList(_context.Categories, "Id",
                "CategoryName", vM.CategoryDTO.ParentCategoryId);
            return View(vM);
        }


        // GET: Categories/Details/5


       // [Authorize(Policy = "managerPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c=>c.ChildCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        public async Task<IActionResult> ProductsByCategoryChild(int? id, int page = 1)
        {
            int itemsPerPage = 3;
            IQueryable<Product> products = _context.Products
                .Include(c=>c.Category)
                .Where(p => p.CategoryId == id)
                .Include(b => b.Brand)
                .Include(i => i.ProductImages);

            int productsCount = products.Count();
            int totalPages = (int)Math.Ceiling((float)productsCount / itemsPerPage);
            products = products.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            ProductsCategoriesVM catVM = new ProductsCategoriesVM()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Products = await products.ToListAsync()
            };
            return View(catVM);
        }


        //[Authorize(Roles = "manager")]
        //[Authorize(Policy = "managerPolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[Authorize(Policy = "managerPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.ParentCategoryId = new SelectList(_context.Categories, "Id", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            ViewBag.ParentCategoryId = new SelectList(_context.Categories, "Id", "CategoryName", category.ParentCategoryId);
            return View(category);
        }
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

    }
}
