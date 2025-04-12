using ShopLibrary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamWebShop.Models.DTOs.Products;

namespace TeamWebShop.Models.DTOs.ProductImages
{
    public class ProductImageDTO
    {
        public int Id { get; set; }

        [Display(Name = "Image(s)")]
        public byte[] ImageData { get; set; } = default!;

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Display(Name = "Product")]
        public ProductDTO? Product { get; set; } = default!;
    }
}
