using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using TeamWebShop.Data;
using TeamWebShop.Extensions.MySessionExtensions;
using TeamWebShop.Models.ViewModels.Cart;

namespace TeamWebShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext context;
        string key = "cart";
        public CartController(ShopContext context)
        {
            this.context = context;
        }

        public IActionResult Index(Cart cart, string? returnUrl)
        {
            //Cart cart = GetCart();
            CartIndexVM vM = new CartIndexVM()
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(vM);
        }
        public async Task<IActionResult> AddToCart(int? id, Cart cart, string? returnUrl)
        {
            if (id == null)
                return NotFound();
            Product? product = await context.Products
                .Include(t => t.Brand)
                .Include(t => t.ProductImages)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (product == null)
                return NotFound();
            //Cart cart = GetCart();
            cart.Add(new CartItem { Product = product, Count = 1 });
            //HttpContext.Session.Set(key, cart.CartItems);
            return RedirectToAction("Index", new {returnUrl});
        }

        [HttpPost]
        public IActionResult RemoveFromCart(Cart cart, int? id, string? returnUrl)
        {
            if (id == null)
                return NotFound();
            cart.Remove(id.Value); // Deleting item from the cart
            //HttpContext.Session.Set(key, cart.CartItems); // Saving data in session to update `Cart` after deleting item
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult Show() // For checking only
        {
            string? userName = HttpContext.Session.GetString("CurrentUser");
            return View(model: userName);
        }

        public IActionResult SetUser()
        {
            HttpContext.Session.SetString("CurrentUser", "A");
            return View();
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return View();
        }

        //public Cart GetCart()
        //{
        //    List<CartItem>? cartItems = 
        //        HttpContext.Session.Get<List<CartItem>>(key);

        //    if (cartItems == null)
        //    {
        //        cartItems = new List<CartItem>();
        //        HttpContext.Session.Set(key, cartItems);
        //    }                
        //    Cart cart = new Cart(cartItems);
        //    return cart;
        //}
    }
}