using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.ViewModels.ProductImages
{
    public class CreateImageVM
    {
        [Display(Name = "Brands")]
        public SelectList? Brands { get; set; }

        [Display(Name = "Please choose brand")]
        public int? SelectedBrandId { get; set; } = default!;

        [Display(Name = "Categories")]
        public SelectList? Categories { get; set; }

        [Display(Name = "Please choose category")]
        public int? SelectedCategoryId { get; set; }

        [Display(Name = "Products")]
        public SelectList? Products { get; set; }

        [Display(Name = "Product")]
        public int SelectedProductId { get; set; }

        [Display(Name = "Photo")]
        public IFormFile[] Photos { get; set; } = default!;
    }
}
