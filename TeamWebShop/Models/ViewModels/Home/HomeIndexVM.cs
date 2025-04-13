using ShopLibrary;
using System.Runtime.InteropServices;

namespace TeamWebShop.Models.ViewModels.Home
{
    public class HomeIndexVM
    {
        public int CurrnetPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<Product>? Products { get; set; }

    }
}
