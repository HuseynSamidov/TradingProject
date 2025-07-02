using Microsoft.EntityFrameworkCore;
using TradingProject.Domain.Entities;

namespace TradingProject.Persistence.Contexts;

public class TradingDbCotext : DbContext
{
    public TradingDbCotext(DbContextOptions<TradingDbCotext> options ):base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TradingDbCotext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Review> Review { get; set; }
    public DbSet<Chat> Chats { get; set; }
}
