using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//Addings
string connStr = builder.Configuration.GetConnectionString("RozegroShopDb") ?? throw new InvalidCastException("Connection string not configured!");
















var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
