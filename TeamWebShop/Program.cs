using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TeamWebShop.Data;

var builder = WebApplication.CreateBuilder(args);

///////////////////////
//Addings
string connStr = builder.Configuration.GetConnectionString("AivenDb") ?? 
    throw new InvalidCastException("Connection string not configured!");

builder.Services.AddDbContext<ShopContext>(options => 
options.UseSqlServer(connStr));

builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<ShopUser, IdentityRole>()
    .AddEntityFrameworkStores<ShopContext>();

////////////////////////////////




var app = builder.Build();

////////////////////////
// Addings
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


/////////////////////////////
app.Run();
