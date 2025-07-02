namespace TradingProject.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingProject.Domain.Entities;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        //builder.ToTable("Categories");

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(c => c.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
