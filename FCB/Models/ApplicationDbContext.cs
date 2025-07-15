using FCB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FCB.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<People> People { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<People>()
                .HasIndex(p => p.Email)
                .IsUnique()
                .HasDatabaseName("IX_People_Email");
            modelBuilder.Entity<People>()
                .HasIndex(p => p.PhoneNumber)
                .IsUnique()
                .HasDatabaseName("IX_People_PhoneNumber");
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Owner)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.OwnerId);
        }
    }
}
