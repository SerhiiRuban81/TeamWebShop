using Microsoft.AspNetCore.Mvc;
using ShopLibrary;
using TeamWebShop.Extensions.MySessionExtensions;

namespace TeamWebShop.ViewComponents
{
    public class CartDetailsViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly string key = "cart";

        public CartDetailsViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<CartItem>? cartItems = null;
            if (httpContextAccessor.HttpContext is not null)
            {
                cartItems = httpContextAccessor.HttpContext
                    .Session.Get<IEnumerable<CartItem>>(key);
                if (cartItems == null)
                    cartItems = new List<CartItem>();
            }
            else
                cartItems = new List<CartItem>();
            Cart cart = new Cart(httpContextAccessor, cartItems!.ToList());
            return View(cart);
        }
    }
}
