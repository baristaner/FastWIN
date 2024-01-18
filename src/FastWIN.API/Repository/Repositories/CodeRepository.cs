using fastwin.Entities;
using fastwin.Infrastructure.UnitOfWork;
using fastwin.Interfaces;
using fastwin.Models;
using Microsoft.EntityFrameworkCore;

namespace fastwin.Repository.Repositories
{
    public class CodeRepository<TEntity> : GenericRepository<TEntity>, ICodeRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CodeDbContext _context;
        private readonly IUnitOfWork _unitOfWork; 

        public CodeRepository(CodeDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork; 
        }

        public async Task<Codes> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Codes>().FirstOrDefaultAsync(c => c.Code == code, cancellationToken);
        }

        public async Task<IEnumerable<Codes>> GetLockedCodesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Codes>().Where(c => c.Status == StatusCode.Locked).ToListAsync(cancellationToken);
        }
    }
}

