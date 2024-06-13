using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
        }

    }
    public async Task<Product> AddAsync(Product entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();

        return entity;

        // Çok sık erişilecek ama çok sık değişmeyecek bir data cache için uygundur.


    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();

        return entities;
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            throw new DirectoryNotFoundException($"{typeof(Product).Name}({id}) not found");
        }

        return Task.FromResult(product);



    }

    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
    {
        var products = await _repository.GetProductsWithCategory();
        var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

        return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto);
        
    }

    public async Task RemoveAsync(Product entity)
    {
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        _repository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
    }
    
    public async Task CacheAllProductsAsync()
    {
        _memoryCache.Set(CacheProductKey, _repository.GetAll().ToListAsync());
    }
}
