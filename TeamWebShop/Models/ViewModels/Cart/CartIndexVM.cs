namespace TeamWebShop.Models.ViewModels.Cart
    
{
    public class CartIndexVM
    {
        public ShopLibrary.Cart Cart { get; set; } = default!;

        public string? ReturnUrl { get; set; }


    }
}
