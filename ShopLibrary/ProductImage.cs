using System.ComponentModel.DataAnnotations.Schema;

namespace ShopLibrary
{
    public class ProductImage
    {
        public int Id { get; set; }

        public byte[]? ImageData { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
