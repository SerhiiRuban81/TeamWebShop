using Microsoft.AspNetCore.Identity;

namespace TeamWebShop.Data
{
    public class ShopUser : IdentityUser
    {
        public bool IsPrivatePerson { get; set; }
    }
}
