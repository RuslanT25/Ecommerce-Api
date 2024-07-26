using Ecommerce.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecommerce.Persistence.Context;

public class EcommerceDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public EcommerceDbContext() { }
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Detail> Details { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}