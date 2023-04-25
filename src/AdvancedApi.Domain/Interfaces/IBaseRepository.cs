using AdvancedApi.Domain.Entities;
using System.Linq.Expressions;

namespace AdvancedApi.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetManyWhere(Expression<Func<T, bool>> condition);
    Task<T> GetOneWhere(Expression<Func<T, bool>> condition);
    Task<bool> Exists(Expression<Func<T, bool>> condition);

    Task Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}