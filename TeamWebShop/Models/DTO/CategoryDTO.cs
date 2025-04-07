using ShopLibrary;
using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Display(Name = "Category name")]
        public string CategoryName { get; set; } = default!;

        [Display(Name = "Parent Category")]
        public int? ParentCategoryId { get; set; }
        public CategoryDTO? ParentCategory { get; set; }

        public ICollection<CategoryDTO> ChildCategories { get; set; } = default!;

        public ICollection<Product> Products { get; set; } = default!;
    }
}
