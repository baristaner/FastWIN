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

        /* Change tracker mekanizması tarafından izlenen her entity nesnesinin bilgisini
        * EntityEntry türünden elde etmemizi sağlar ve belirli işlemler yapabilmemize olanak tanır.
        * Entries metodu,DetectChanges metodunu tetikler
        * NOT : AutoDetectChangesEnabled özelliği 
        */
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime now = DateTime.UtcNow;
            Console.WriteLine("SaveChangesAsync called");

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedAt = now;
                    Console.WriteLine($"Entity Type: {entry.Entity.GetType().Name}, State: {entry.State}, ModifiedAt: {entry.Entity.ModifiedAt}");
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
