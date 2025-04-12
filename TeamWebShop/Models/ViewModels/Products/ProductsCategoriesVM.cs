using ShopLibrary;

namespace TeamWebShop.Models.ViewModels.Products
{
    public class ProductsCategoriesVM
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
