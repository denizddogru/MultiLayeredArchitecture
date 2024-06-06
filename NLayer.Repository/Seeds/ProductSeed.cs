using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Kalem 1",
                    Price = 100,
                    Stock = 20,
                    CreatedDate = DateTime.Now,
                },
                  new Product
                  {
                      Id = 2,
                      CategoryId = 2,
                      Name = "Kitap 1",
                      Price = 200,
                      Stock = 20,
                      CreatedDate = DateTime.Now,
                  },
                   new Product
                   {
                       Id = 3,
                       CategoryId = 2,
                       Name = "Kitap 2",
                       Price = 125,
                       Stock = 35,
                       CreatedDate = DateTime.Now,
                   },
                    new Product
                    {
                        Id = 4,
                        CategoryId = 1,
                        Price = 500,
                        Name = "Kalem 3",
                        Stock = 10,
                        CreatedDate = DateTime.Now,
                    });

        }
    }
}
