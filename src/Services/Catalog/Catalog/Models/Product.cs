using Catalog.Products.CreateProduct;
using Catalog.Products.UpdateProduct;

namespace Catalog.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
    public List<string> Categories { get; set; } = new();

    public void Create(CreateProductCommand command)
    {
        Id = Guid.NewGuid();
        Name = command.Name;
        Description = command.Description;
        ImageFile = command.ImageFile;
        Price = command.Price;
        Categories = command.Categories;
    }
    
    public void Update(UpdateProductCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        ImageFile = command.ImageFile;
        Price = command.Price;
        Categories = command.Categories;
    }
}