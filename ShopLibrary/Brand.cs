using System.ComponentModel.DataAnnotations;

namespace ShopLibrary
{
    public class Brand
    {
        public int Id { get; set; }

        [Display(Name = "Brand name")]
        public string BrandName { get; set; } = default!;

        [Display(Name = "Country")]
        public string Country { get; set; } = default!;

        public ICollection<Product>? Products { get; set; } = default!;
    }
}
