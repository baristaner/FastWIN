using fastwin.Entities;
using fastwin.Models;
using Microsoft.EntityFrameworkCore;

namespace fastwin
{
    public class CodeDbContext : DbContext
    {
        public DbSet<Codes> Codes { get; set; }

        public CodeDbContext(DbContextOptions<CodeDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.CreatedAt == DateTime.MinValue)
                        {
                            entry.Entity.CreatedAt = now;
                        }
                        entry.Entity.ModifiedAt = now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = now;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
