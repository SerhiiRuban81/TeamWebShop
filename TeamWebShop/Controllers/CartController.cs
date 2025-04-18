using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using TeamWebShop.Data;
using TeamWebShop.Extensions.MySessionExtensions;

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

        public IActionResult Index(Cart cart)
        {
            //Cart cart = GetCart();
            return View(cart);
        }
        public async Task<IActionResult> AddToCart(int? id, Cart cart)
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
            HttpContext.Session.Set(key, cart.CartItems);
            return RedirectToAction("Index");
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

        public Cart GetCart()
        {
            List<CartItem>? cartItems = 
                HttpContext.Session.Get<List<CartItem>>(key);

            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
                HttpContext.Session.Set(key, cartItems);
            }                
            Cart cart = new Cart(cartItems);
            return cart;
        }
    }
}