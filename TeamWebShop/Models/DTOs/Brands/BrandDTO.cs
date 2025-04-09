using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.DTOs.Brands
{
    public class BrandDTO
    {
        public int Id { get; set; }
        [Display(Name = "Brand name")]
        public string BrandName { get; set; } = default!;
        [Display(Name = "Country")]
        public string Country { get; set; } = default!;
    }
}
