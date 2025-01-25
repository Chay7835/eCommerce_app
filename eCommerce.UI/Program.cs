using eCommerce.Dal;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddGoogle(options =>
 {
     IConfigurationSection googleAuthNSection =
     builder.Configuration.GetSection("Authentication:Google");
     options.ClientId = googleAuthNSection["ClientId"];
     options.ClientSecret = googleAuthNSection["ClientSecret"];
     options.ClaimActions.MapJsonKey("urn:google:picture", "picture");
     options.ClaimActions.MapJsonKey("urn:google:email", "email");
 }).AddCookie();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
    // Indicates whether the website is essential for the website
    // So we can Bypass that consent section(permissions)
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

// Connectiion to that key in appsettings.json
builder.Services.AddDbContext<EcommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("eCommerceConStr"));
});

// Adding the DbContext to the Service
builder.Services.AddDbContext<EcommerceDbContext>();

// Singleton Instance for Product(So that everybody will get the same instance to display
// the products, etc 
builder.Services.AddTransient<ICommonRepository<Product>,CommonRepository<Product>>();
builder.Services.AddTransient<ICommonRepository<Category>, CommonRepository<Category>>();
builder.Services.AddTransient<ICommonRepository<CartDetail>, CommonRepository<CartDetail>>();
// builder.Services.AddTransient<ICommonRepository<Cart>, CommonRepository<Cart>>();

builder.Services.AddTransient<INewCartRepository,NewCartRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())

{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "ProductsManager",
        areaName: "Products",
        pattern: "Products/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapAreaControllerRoute(
        name: "categoriesmanager",
        areaName: "categories",
        pattern: "categories/{controller=home}/{action=index}/{id?}"
    );

    endpoints.MapAreaControllerRoute(
        name: "SecurityManager",
        areaName: "Security",
        pattern: "Security/{controller=Home}/{action=Login}/{id?}"
    );

    endpoints.MapAreaControllerRoute(
        name: "CartsManager",
        areaName: "Carts",
        pattern: "Carts/{controller=Home}/{action=MyCart}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

});

app.Run();
