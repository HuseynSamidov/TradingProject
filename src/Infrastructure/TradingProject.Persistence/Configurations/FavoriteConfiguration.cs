using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingProject.Domain.Entities;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        // Table name (optional)
        //builder.ToTable("Favorites");

        // Unique constraint: UserId + ProductId
        builder.HasIndex(f => new { f.UserId, f.ProductId })
               .IsUnique();

        // Relation: Favorite -> User (Many-to-One)
        builder.HasOne(f => f.User)
               .WithMany(u => u.Favorites)
               .HasForeignKey(f => f.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relation: Favorite -> Product (Many-to-One)
        builder.HasOne(f => f.Product)
               .WithMany(p => p.Favorites)
               .HasForeignKey(f => f.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
