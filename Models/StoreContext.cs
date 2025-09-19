using Microsoft.EntityFrameworkCore;
namespace OnlineStore.Api.Models;
public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
}
