using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Drawing.Drawing2D;
using ShopLibrary;
using TeamWebShop.Models.DTOs.Admin;
using TeamWebShop.Models.DTOs.Users;
using TeamWebShop.Models.ViewModels.Users;

namespace TeamWebShop.Data
{
    public class ShopContext : IdentityDbContext<ShopUser>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        { }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> Images { get; set; }
        public DbSet<TeamWebShop.Models.DTOs.Users.UserDTO> UserDTO { get; set; } = default!;
       
       
       
    }
}
