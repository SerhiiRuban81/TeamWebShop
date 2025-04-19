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

        public bool Remove(CartItem item)
        {
            return items.Remove(item);
        }

        public bool Remove(int id)
        {
            CartItem? cartItem = items.FirstOrDefault(t => t.Product.Id == id);
            if(cartItem != null)
                return items.Remove(cartItem);
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
    }
}
