namespace TradingProject.Domain.Entities;

public class User : BaseEntity 
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public ICollection<Favorite> Favorites { get; set; } 
}
