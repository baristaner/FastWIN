using fastwin.Entities;
using fastwin.Infrastructure.UnitOfWork;
using fastwin.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;


namespace fastwin.Repository.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CodeDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(CodeDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task ExecuteSqlAsync(string sql, CancellationToken cancellationToken = default, params object[] parameters)
        {
          await _context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ExecuteSqlQueryAsync<TEntity>(string sql, CancellationToken cancellationToken = default, params object[] parameters) where TEntity : class
        {
            return await _context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync(cancellationToken);
        }
    }
}
