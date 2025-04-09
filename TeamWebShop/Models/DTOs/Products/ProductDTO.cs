using ShopLibrary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamWebShop.Models.DTOs.Brands;
using TeamWebShop.Models.DTOs.Categories;

namespace TeamWebShop.Models.DTOs.Products
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Display(Name = "Product name")]
        public string ProductName { get; set; } = default!;
        [Display(Name = "Description")]
        public string Description { get; set; } = default!;
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Display(Name = "Brand")]
        public BrandDTO? Brand { get; set; } = default!;

        [Display(Name = "Category")]
        public CategoryDTO? Category { get; set; } = default!;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Main category")]
        public int? ParentCategoryId { get; set; }

        public ICollection<ProductImage>? ProductImages { get; set; } = default!;

        public string? Color { get; set; }

        [Display(Name = "Product Model")]
        public string? ProductModel { get; set; }
    }
}
