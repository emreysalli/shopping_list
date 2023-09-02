using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using emre_yasin_salli_thesis.Data;
using emre_yasin_salli_thesis.Areas.Identity.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShoppingListDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ShoppingListDbContextConnection' not found.");

builder.Services.AddDbContext<ShoppingListDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ShoppingListDbContext>().AddRoles<IdentityRole>()
.AddDefaultUI().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/User/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=user}/{action=shoppinglist}/{id?}");

app.MapRazorPages();

app.Run();
