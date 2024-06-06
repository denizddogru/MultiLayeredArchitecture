using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using System.Drawing;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                    Id=1,
                    Color="Red",
                    Height=50,
                    Width=50,
                    ProductId=1
            },

            new ProductFeature()
            {
                Id=2,
                Color="Blue",
                Height=530,
                Width=530,
                ProductId=2
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}