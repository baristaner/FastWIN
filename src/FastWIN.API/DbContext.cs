using fastwin.Entities;
using fastwin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace fastwin
{
    public class CodeDbContext : IdentityDbContext<User>
    {
        public DbSet<Codes> Codes { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Users> Users {  get; set; }
        public DbSet<UserCode> UserCode { get; set; }
        public DbSet<Asset> Asset { get; set; }

      
        public CodeDbContext(DbContextOptions<CodeDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserCode>()
            .HasOne(uc => uc.Code)
            .WithMany() // No navigation property in Codes pointing back to UserCode
            .HasForeignKey(uc => uc.CodeId);

            base.OnModelCreating(builder);
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

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.ModifiedAt = now;
                }

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