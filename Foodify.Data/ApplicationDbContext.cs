using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Foodify.Data.Entities;

namespace Foodify.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    // public DbSet<User> Users { get; set; } // Handled by IdentityDbContext
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryPartner> DeliveryPartners { get; set; }
        public DbSet<DeliveryAssignment> DeliveryAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Restaurant
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Restaurant - Menu
            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Restaurant)
                .WithMany(r => r.Menus)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order - OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Menu - OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(oi => oi.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - DeliveryPartner
            modelBuilder.Entity<DeliveryPartner>()
                .HasOne(dp => dp.User)
                .WithMany()
                .HasForeignKey(dp => dp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // DeliveryPartner - DeliveryAssignment
            modelBuilder.Entity<DeliveryAssignment>()
                .HasOne(da => da.DeliveryPartner)
                .WithMany(dp => dp.DeliveryAssignments)
                .HasForeignKey(da => da.DeliveryPartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order - DeliveryAssignment
            modelBuilder.Entity<DeliveryAssignment>()
                .HasOne(da => da.Order)
                .WithMany(o => o.DeliveryAssignments)
                .HasForeignKey(da => da.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
