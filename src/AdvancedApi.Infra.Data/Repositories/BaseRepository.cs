using AdvancedApi.Domain.Interfaces;
using AdvancedApi.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdvancedApi.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AdvancedContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(AdvancedContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task Add(T entity)
        => await _dbSet.AddAsync(entity);

    public void Update(T entity)
        => _dbSet.Update(entity);

    public void Delete(T entity)
        => _dbSet.Remove(entity);

    public async Task<bool> Exists(Expression<Func<T, bool>> condition)
        => await _dbSet.FirstOrDefaultAsync(condition) is not null;

    public async Task<IEnumerable<T>> GetAll()
        => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> GetManyWhere(Expression<Func<T, bool>> condition)
        => await _dbSet.Where(condition).ToListAsync();

    public async Task<T> GetOneWhere(Expression<Func<T, bool>> condition)
        => (await _dbSet.FirstOrDefaultAsync(condition))!;
}