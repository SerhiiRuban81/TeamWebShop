using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using TeamWebShop.Data;
using TeamWebShop.Models.ViewModels.Categories;

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
            var shopContext = _context.Categories.Include(c => c.ParentCategory);
            return View(await shopContext.ToListAsync());
        }

        // GET: Categories/Create
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
        public async Task<IActionResult> Details(int? id)
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

    }
}
