using emre_yasin_salli_thesis.Areas.Identity.Data;
using emre_yasin_salli_thesis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace emre_yasin_salli_thesis.Data;

public class ShoppingListDbContext : IdentityDbContext<ApplicationUser>
{
    public ShoppingListDbContext()
    {
    }

    public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
    public virtual DbSet<ShoppingListDetail> ShoppingListDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Seed();

        

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=ShoppingListDb;Trusted_Connection=True;Encrypt=False");
}
