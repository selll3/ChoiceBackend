using System.Linq;
using System.Linq.Expressions;
using Choice_Ym.Abstractions;
using Choice_Ym.Data;
using Choice_Ym.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice_Ym.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityModel, new()
    {
        private readonly ChoiceymDbContext _context;
        private IQueryable<T> _query;
        public Repository(ChoiceymDbContext context)
        {
            _context = context;
        }

        private DbSet<T> Table { get => _context.Set<T>(); }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await Table.AddAsync(entity);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(List<T> entity)
        {
            try
            {
                await Table.AddRangeAsync(entity);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<List<T>> GetAllAsync()
            => await Table.ToListAsync();

        public async Task<T> GetByIdAsync(int? id)
            => await Table.FindAsync(id);

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                Table.Remove(entity);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                Table.Update(entity);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveChangesAsync()
            => await _context.SaveChangesAsync() > 0;

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            _query = Table.AsQueryable();

            if (predicate != null)
                _query = _query.Where(predicate);

            return await _query.SingleOrDefaultAsync();
        }
    }
}