using ShopLibrary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebShop.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = default!;

        public string Description { get; set; } = default!;

        public double Price { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public BrandDTO Brand { get; set; } = default!;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; } = default!;

        public ICollection<ProductImage> ProductImages { get; set; } = default!;

        public string? Color { get; set; }

        [Display(Name = "Product Model")]
        public string? ProductModel { get; set; }
    }
}
