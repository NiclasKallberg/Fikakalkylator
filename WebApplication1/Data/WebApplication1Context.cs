using Azure;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context(DbContextOptions<WebApplication1Context> options) : DbContext(options)
    {



        // Tillagt av mig start
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));

            modelBuilder.Entity<Product>().ToTable(nameof(Product));

            modelBuilder.Entity<Purchase>().ToTable(nameof(Purchase))
                .HasOne(u => u.User).WithMany();

            modelBuilder.Entity<Purchase>().ToTable(nameof(Purchase))
                .HasOne(p => p.Product).WithMany();

            modelBuilder.Entity<PositionDuration>().ToTable(nameof(PositionDuration))
                .HasOne(u => u.User).WithMany();

        }

        // Tillagt av mig slut





        // Ändrat från singular till plural och default till null från hur det var från början
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Purchase> Purchases { get; set; } = null!;
        public DbSet<PositionDuration> PositionDurations { get; set; } = null!;
    }
}
