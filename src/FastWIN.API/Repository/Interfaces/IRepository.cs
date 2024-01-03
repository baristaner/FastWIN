using fastwin.Entities;
using System.Linq.Expressions;

namespace fastwin.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task ExecuteStoredProcedureAsync(string sql, params object[] parameters);
        Task<IEnumerable<TEntity>> ExecuteSqlQueryAsync<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}
