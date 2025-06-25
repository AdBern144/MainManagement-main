using Microsoft.EntityFrameworkCore;

namespace StockManagement.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    //public DbSet<Customer> Customers {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.User)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade); //  optioneel

        //modelBuilder.Entity<Customer>()
        //    .HasOne(p => p.User)
        //    .WithMany(u => u.Customers)
        //    .HasForeignKey(p => p.UserId)
        //    .OnDelete(DeleteBehavior.Cascade); //  optioneel
    }

}
