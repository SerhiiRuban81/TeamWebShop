using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TeamWebShop.Data;
using TeamWebShop.Profiles;

var builder = WebApplication.CreateBuilder(args);

///////////////////////
//Addings
string connStr = builder.Configuration.GetConnectionString("AivenDb") ??
    throw new InvalidCastException("Connection string not configured!");

builder.Services.AddDbContext<ShopContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
    else
        options.UseSqlServer(connStr);
}
);


builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<ShopUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ShopContext>();

builder.Services.AddAutoMapper(typeof(UserProfile));


////////////////////////////////




var app = builder.Build();

////////////////////////
// Addings
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");


/////////////////////////////
app.Run();
