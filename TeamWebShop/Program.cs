using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TeamWebShop.Data;
using TeamWebShop.Profiles;


var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddIdentity<ShopUser, IdentityRole>()
    .AddEntityFrameworkStores<ShopContext>();


builder.Services.AddAutoMapper(typeof(BrandProfile));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
