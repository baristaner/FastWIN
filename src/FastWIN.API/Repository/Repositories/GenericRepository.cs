using fastwin.Entities;
using fastwin.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace fastwin.Repository.Repositories 
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CodeDbContext _context;

        public GenericRepository(CodeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ExecuteStoredProcedureAsync(string sql, params object[] parameters) 
        {
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<IEnumerable<TEntity>> ExecuteSqlQueryAsync<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return await _context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();
        }
    }
}
