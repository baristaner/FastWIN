using fastwin.Entities;
using fastwin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fastwin
{
    public class CodeDbContext : IdentityDbContext<User>
    {
        public DbSet<Codes> Codes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserCode> UserCode { get; set; }
        public DbSet<Asset> Asset { get; set; }

      
        public CodeDbContext(DbContextOptions<CodeDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.Entity<Asset>()
           .HasOne(asset => asset.Product)
           .WithMany()
           .HasForeignKey(asset => asset.ProductId);

           builder.Entity<Asset>()
            .HasOne(asset => asset.Codes)
            .WithMany()
            .HasForeignKey(asset => asset.CodeId);

            builder.Entity<Asset>()
            .HasOne(asset => asset.User)
            .WithMany()
            .HasForeignKey(asset => asset.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserCode>()  
            .HasOne(uc => uc.Code)
            .WithMany() 
            .HasForeignKey(uc => uc.CodeId);

            builder.Entity<UserCode>()
            .HasOne(uc => uc.User)
            .WithMany() 
            .HasForeignKey(uc => uc.UserId);

            builder.Entity<Codes>() 
                .HasIndex(uc => uc.Code).IsClustered(false);

            base.OnModelCreating(builder);
        }

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