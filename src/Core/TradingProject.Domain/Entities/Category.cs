namespace TradingProject.Domain.Entities;

public class Category : BaseEntity
{

    public string Name { get; set; } = null!;//amma niyese =null! olanda partdiyir
    //public required string Name { get; set; } olanda CategoryRepository error verir(ayrica baxarsan)
    public ICollection<Product> Products { get; set; }

}
