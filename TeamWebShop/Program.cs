using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TeamWebShop.Data;
using TeamWebShop.Infrastructure.BinderProviders;
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

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new CartModelBinderProvider());
});
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



builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddAutoMapper(typeof(UserProfile), typeof(RoleProfile), typeof(BrandProfile));



var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
