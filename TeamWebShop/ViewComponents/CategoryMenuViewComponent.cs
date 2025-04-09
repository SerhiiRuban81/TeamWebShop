using Microsoft.AspNetCore.Mvc;
using TeamWebShop.Data;
using Microsoft.EntityFrameworkCore;


namespace TeamWebShop.ViewComponents
{
    public class CategoryMenuViewComponent: ViewComponent
    {
        private readonly ShopContext _context;

        public CategoryMenuViewComponent(ShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                .Where(c => c.ParentCategory == null)
                .Include(p => p.ParentCategory)
                .Include(c=> c.ChildCategories).ToListAsync();
            return View(categories);
        }
    }
}
