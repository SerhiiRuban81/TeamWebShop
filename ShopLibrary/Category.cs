using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLibrary
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Category name")]
        public string CategoryName { get; set; } = default!;

        public int? ParentCategoryId { get; set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public Category? ParentCategory { get; set; }

        public ICollection<Category>? ChildCategories { get; set; } = default!;

        public ICollection<Product>? Products { get; set; } = default!;

        //щоб витягти фото в категорію
        [NotMapped]
        public byte[]? ImageData { get; set; }

    }
}
