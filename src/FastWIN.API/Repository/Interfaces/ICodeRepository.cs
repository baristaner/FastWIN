using fastwin.Entities;
using fastwin.Models; 

namespace fastwin.Interfaces
{
    public interface ICodeRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<Codes> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<IEnumerable<Codes>> GetLockedCodesAsync(CancellationToken cancellationToken = default);
    }
}
