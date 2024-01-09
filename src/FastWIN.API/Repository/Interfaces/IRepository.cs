using fastwin.Entities;
using System.Linq.Expressions;

namespace fastwin.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task ExecuteStoredProcedureAsync(string sql, CancellationToken cancellationToken = default, params object[] parameters);
        Task<IEnumerable<TEntity>> ExecuteSqlQueryAsync<TEntity>(string sql, CancellationToken cancellationToken = default, params object[] parameters) where TEntity : class;
    }
}
