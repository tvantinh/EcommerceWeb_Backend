using Ecommecre_BE.Repositories.DataContext;
using Microsoft.EntityFrameworkCore;
using Ecommerce_BE.Contract.Repositories.Repositories;

namespace Ecommecre_BE.Repositories.UnitOfWork
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Entities => _dbSet;

        public async Task AddAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public async Task DeleteAsync(object id)
        {
            T? entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            _dbSet.Remove(entity);
        }
        public async Task UpdateAsync(T obj)
        {
            _dbSet.Update(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T obj)
        {
            _dbSet.Update(obj);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
