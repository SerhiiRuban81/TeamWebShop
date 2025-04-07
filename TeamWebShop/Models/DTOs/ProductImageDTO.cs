using ShopLibrary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebShop.Models.DTOs
{
    public class ProductImageDTO
    {
        public int Id { get; set; }

        [Display(Name = "Image(s)")]
        public byte[] ImageData { get; set; } = default!;

        [Display(Name = "Product")]
        public int ProductId { get; set; }
    }
}
