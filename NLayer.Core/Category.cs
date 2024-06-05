namespace NLayer.Core;

public class Category : BaseEntity
{
    public string Name { get; set; }

    // Category'nin artık birden çok products'ı var.
    // referans verdiğimiz propertylere navigation property denir.
    public ICollection<Product> Products { get; set; }

}
