using fastwin.Entities;
using fastwin.Interfaces;

namespace fastwin.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly CodeDbContext _context;
        public ProductRepository(CodeDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
