using System.Linq.Expressions;

namespace NLayer.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
    //Tolist, ToListAsync gibi metodları çağrırken döner
    // productRepository.where(x => x.Id>5).OrderBy.ToListAsync();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    IQueryable<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    // Update işleminde sadece state değiştiği için asenkron bir işlem olmasına gerek yoktur.
    void Update(T entity);
    void Delete(T entity);
    void RemoveRange(IEnumerable<T> entities);


}
