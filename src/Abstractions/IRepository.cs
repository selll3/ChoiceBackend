using System.Linq.Expressions;
using Choice_Ym.Models;

namespace Choice_Ym.Abstractions
{
    public interface IRepository<T> where T : EntityModel, new()
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entity);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(int id);
        bool Update(T entity);
        Task<bool> SaveChangesAsync();
    }
}