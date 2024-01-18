using fastwin.Entities;
using fastwin.Models;  // Make sure to import the correct namespace

namespace fastwin.Interfaces
{
    public interface ICodeRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<Codes> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
