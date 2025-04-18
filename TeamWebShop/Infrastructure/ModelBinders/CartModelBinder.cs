using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopLibrary;
using System.Diagnostics;
using TeamWebShop.Extensions.MySessionExtensions;

namespace TeamWebShop.Infrastructure.ModelBinders
{
    public class CartModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string key = "cart";
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));
            List<CartItem>? cartItems =
                 bindingContext.HttpContext.Session.Get<List<CartItem>>(key);
            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
                bindingContext.HttpContext.Session.Set(key, cartItems);
            }
            //Cart cart = new Cart(httpContextAccessor, cartItems);
            Cart cart = new Cart(cartItems);
            bindingContext.Result = ModelBindingResult.Success(cart);
            return Task.CompletedTask;
        }
    }
}
