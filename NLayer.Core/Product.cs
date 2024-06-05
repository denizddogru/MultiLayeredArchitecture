namespace NLayer.Core;

public class Product : BaseEntity
{

    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ProductFeature? ProductFeature { get; set; }
}