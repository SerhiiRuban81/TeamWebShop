using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWebShop.Extensions.MySessionExtensions;


namespace ShopLibrary
{
    public class Cart
    {
        readonly string key = "cart";
        List<CartItem> items = default!;
        private readonly IHttpContextAccessor httpContextAccessor;

        //private readonly IHttpContextAccessor httpContextAccessor;

        public List<CartItem> CartItems => items;

        public int ItemsCount => CartItems.Count;

        public Cart(IHttpContextAccessor httpContextAccessor, List<CartItem> items)
        {
            this.items = items;
            this.httpContextAccessor = httpContextAccessor;
        }

        //public void SetItems(List<CartItem> items)
        //{
        //    this.items = items;
        //}

        //public Cart(IHttpContextAccessor httpContextAccessor)
        //{
        //    this.httpContextAccessor = httpContextAccessor;
        //}

        public void Add(CartItem item)
        {
            CartItem? cartItem = items.FirstOrDefault(t => t.Product.Id == item.Product.Id);
            if (cartItem == null)
            {
                items.Add(item);                
            }                
            else
                cartItem.Count = cartItem.Count + 1;
            if(httpContextAccessor.HttpContext is not null)
                httpContextAccessor.HttpContext!.Session.Set(key, CartItems);
        }

        public bool Remove(CartItem item)
        {
            bool result = items.Remove(item);
            httpContextAccessor.HttpContext!.Session.Set(key, this.CartItems);
            return result;
        }

        public bool Remove(int id)
        {
            CartItem? cartItem = items.FirstOrDefault(t => t.Product.Id == id);
            if (httpContextAccessor.HttpContext is not null)
                httpContextAccessor.HttpContext!.Session.Set(key, CartItems);
            return false;
        }

        public void Clear()
        {
            items = new List<CartItem>();
        }

        public double GetTotalPrice()
        {
            return items.Sum(t => t.TotalPrice);
        }


        ///add 
        public void DecreaseOne(CartItem item)
        {
            CartItem? cartItem = items.FirstOrDefault(c => c.Product.Id == item.Product.Id);
            if (cartItem != null)
            {
                cartItem.Count = cartItem.Count - 1;
            }
            if (item.Count <= 0)
            {
                CartItems.Remove(item);
            }
            if (httpContextAccessor.HttpContext is not null)
                httpContextAccessor.HttpContext.Session.Set(key, CartItems);
        }

        ////
    }
}
