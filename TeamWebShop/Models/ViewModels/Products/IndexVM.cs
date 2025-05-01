using TeamWebShop.Models.DTOs.Products;

namespace TeamWebShop.Models.ViewModels.Products
{
    public class IndexVM
    {
        public IEnumerable<ProductDTO> Products { get; set; } = default!;
        public string? Search { get; set; } = default!;
    }
}
