using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ECommerce.Models;
using EcommerceProject.DAL.IdentityApplication;
namespace ECommerce.Data
{
    public class EcommerceContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> //to be open for extend close for modification
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }

        // i can't solve the issue
        // to resolve the issue with multiple paths for cascading
        /*
            The Order table has a foreign key to the Car table with ON DELETE CASCADE,
            and other tables like OrderDetails, Shipping, Payment also have foreign keys to Order (some of which may also cascade).
            This creates a scenario where deleting a Car might cascade-delete its Orders,
            and those in turn might cascade-delete OrderDetails, Shipping, and Payment.
            This is a multiple cascade path, which SQL Server doesn't allow.
        */

        // In your DbContext class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fix for Orders -> Cars relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Car)  // Use the actual navigation property
                .WithMany()          // Specify the inverse navigation if it exists
                .HasForeignKey(o => o.CarID)
                .OnDelete(DeleteBehavior.Restrict);

            // Fix for OrderDetails -> Orders relationship
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // Fix for OrderDetails -> Cars relationship
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Car)
                .WithMany()
                .HasForeignKey(od => od.CarID)
                .OnDelete(DeleteBehavior.Restrict);

            // Add similar configurations for other entities with relationships

            // For example, for Reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarID)
                .OnDelete(DeleteBehavior.Restrict);

            // For Wishlist
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Car)
                .WithMany()
                .HasForeignKey(w => w.CarID)
                .OnDelete(DeleteBehavior.Restrict);

            // For Payments
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // For Shipping
            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Order)
                .WithMany()
                .HasForeignKey(s => s.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
