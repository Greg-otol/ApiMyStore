namespace ApiMyStore.Endpoints.Products;

public record ProductRequest(string Name, Guid CategoryId, string Description, decimal Price, string ImageUrl, bool HasStock, bool Active);
