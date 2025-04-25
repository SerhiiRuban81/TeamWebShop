using ShopLibrary;

namespace TeamWebShop.Models.ViewModels.Categories
{
    public class ShowCategoryVM
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
        public IEnumerable<Category>? Categories { get; set; }

        public Category? ParentCategory { get; set; }
        public IEnumerable<Product>? Products { get; set; }

    }
}
