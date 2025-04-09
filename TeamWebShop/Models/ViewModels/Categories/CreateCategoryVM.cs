using Microsoft.AspNetCore.Mvc.Rendering;
using TeamWebShop.Models.DTOs.Categories;

namespace TeamWebShop.Models.ViewModels.Categories
{
    public class CreateCategoryVM
    {
        public CategoryDTO CategoryDTO { get; set; } = default!;

        public SelectList? ParentCategories { get; set; }
    }
}
