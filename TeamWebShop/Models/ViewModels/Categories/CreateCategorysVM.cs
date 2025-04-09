using Microsoft.AspNetCore.Mvc.Rendering;
using TeamWebShop.Models.DTOs;

namespace TeamWebShop.Models.ViewModels.Categories
{
    public class CreateCategorysVM
    {
        public CategoryDTO CategoryDTO { get; set; } = default!;

        public SelectList? ParentCategorys { get; set; }
    }
}
