using Microsoft.EntityFrameworkCore;
using Restaurant.Entity;

namespace Restaurant.AppDb
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions ontions) : base(ontions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(clientId => clientId.Id);

            modelBuilder.Entity<Order>()
                .HasKey(orderId => orderId.Id);

            modelBuilder.Entity<Client>()
                 .HasMany(orderHistory => orderHistory.OrderHistories)
                 .WithOne(client => client.Client)
                 .HasForeignKey(clientId => clientId.ClientId);

            modelBuilder.Entity<Order>()
                 .HasMany(orderHistory => orderHistory.OrderHistories)
                 .WithOne(order => order.Order)
                 .HasForeignKey(orderId => orderId.OrderId); ;

            modelBuilder.Entity<OrderHistory>()
                .HasKey(x => new { x.OrderId, x.ClientId });
        }
    }
}
