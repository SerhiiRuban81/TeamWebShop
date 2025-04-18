using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class Cart
    {
        string key = "cart";
        List<CartItem> items = default!;
        //private readonly IHttpContextAccessor httpContextAccessor;

        public List<CartItem> CartItems => items;

        public Cart(List<CartItem> items)
        {
            this.items = items;
        }

        public void Add(CartItem item)
        {
            CartItem? cartItem = items.FirstOrDefault(t => t.Product.Id == item.Product.Id);
            if (cartItem == null)
                items.Add(item);
            else
                cartItem.Count = cartItem.Count + 1;
        }

        public void Remove(CartItem item)
        {
            items.Remove(item);
        }

        public void Clear()
        {
            items = new List<CartItem>();
        }

        public double GetTotalPrice()
        {
            return items.Sum(t => t.TotalPrice);
        }
    }
}
