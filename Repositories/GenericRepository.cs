using Entities.Data;
using Microsoft.EntityFrameworkCore;
using RepositoryContract;

namespace Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T>_db;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
        }
    }
}
