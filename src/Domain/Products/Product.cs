using ApiMyStore.Domain.Orders;

namespace ApiMyStore.Domain.Products;

public class Product : Entity
{
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string ImageUrl { get; private set; }
    public bool HasStock { get; private set; }
    public bool Active { get; private set; } = true;
    public ICollection<Order> Orders { get; private set; }

    private Product() { }

    public Product(string name, Category category, string description, decimal price, string imageUrl, bool hasStock, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        HasStock = hasStock;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category", "Category not found")
            .IsNotNullOrEmpty(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 10, "Description")
            .IsGreaterOrEqualsThan(Price, 1, "Price")
            .IsNotNullOrEmpty(ImageUrl, "ImageUrl")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }

    private void ValidatePut()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNullOrEmpty(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 10, "Description")
            .IsGreaterOrEqualsThan(Price, 1, "Price")
            .IsNotNullOrEmpty(ImageUrl, "ImageUrl")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }
    public void EditInfo(string name, string description, decimal price , string imageUrl, bool hasStock, bool active, string editedBy)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        HasStock = hasStock;
        Active = active;

        EditedBy = editedBy;
        EditedOn = DateTime.Now;

        ValidatePut();
    }
}
