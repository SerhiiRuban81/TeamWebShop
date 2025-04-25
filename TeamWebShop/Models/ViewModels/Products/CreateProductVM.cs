using Microsoft.AspNetCore.Mvc.Rendering;
using TeamWebShop.Models.DTOs.Products;

namespace TeamWebShop.Models.ViewModels.Products
{
    public class CreateProductVM
    {
        public ProductDTO ProductDTO { get; set; } = default!;
        public SelectList? Brands { get; set; }
        public SelectList? Categories { get; set; }
    }
}
