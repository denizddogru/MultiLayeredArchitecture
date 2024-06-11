using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;

namespace NLayer.Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{

    public ProductRepository(AppDbContext context) : base(context)
    {
        
    }
    public async Task<List<Product>> GetProductsWithCategory()
    {
        // Eager loading: Datayı çekerken, kategorilerin de alınmasını istedik.

        return await _context.Products.Include(x=>x.Category).ToListAsync();
    }
}
