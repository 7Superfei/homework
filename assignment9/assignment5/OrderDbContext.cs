using Microsoft.EntityFrameworkCore;
using System;

namespace OrderManagement
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=orderdb;User=root;Password=supengfei9740;",
                new MySqlServerVersion(new Version(8, 0, 0))
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置Order实体
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey("CustomerId");

            // 配置OrderDetails实体
            modelBuilder.Entity<OrderDetails>()
                .HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey("ProductId");

            modelBuilder.Entity<OrderDetails>()
                .HasOne<Order>()
                .WithMany(o => o.Details)
                .HasForeignKey("OrderId");
        }
    }
} 