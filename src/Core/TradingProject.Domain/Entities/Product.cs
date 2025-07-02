namespace TradingProject.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Favorite> Favorites { get; set; }
}
