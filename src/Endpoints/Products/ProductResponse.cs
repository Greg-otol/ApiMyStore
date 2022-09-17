namespace ApiMyStore.Endpoints.Products;

public record ProductResponse(Guid Id, string Name, string CategoryName, string Description, decimal Price, string ImageUrl, bool HasStock, bool Active);
