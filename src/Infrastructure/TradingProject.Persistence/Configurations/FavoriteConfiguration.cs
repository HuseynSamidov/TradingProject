using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingProject.Domain.Entities;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        //builder.ToTable("Favorites");

        builder.HasIndex(f => new { f.UserId, f.ProductId })
               .IsUnique();

        builder.HasOne(f => f.User)
               .WithMany(u => u.Favorites)
               .HasForeignKey(f => f.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.Product)
               .WithMany(p => p.Favorites)
               .HasForeignKey(f => f.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
