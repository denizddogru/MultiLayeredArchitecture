using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWork;
using System.Linq.Expressions;

namespace NLayer.Caching;
public class ProductServiceWithCaching : IProductService
{

    private const string CacheProductKey = "productsCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _repository = repository;
        _unitOfWork = unitOfWork;

        // sadece bu cache key'ine ait data var mı yok mu diye memory'ye baktık. Alt-tire memory'de yer kaplamıyor.
        if(!_memoryCache.TryGetValue(CacheProductKey, out _)) 
        {
            _memoryCache.Set(CacheProductKey, _repository.GetAll().ToList());
        }

    }
    public Task<Product> AddAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        throw new NotImplementedException();
    }
}
