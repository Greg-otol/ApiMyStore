namespace ApiMyStore.Endpoints.Orders;

public record OrderResponse(
    Guid Id, string Name, string Clientemail,
    IEnumerable<OrderProduct> Products,
    decimal Total,
    string DeliveryAddress);

public record OrderProduct(Guid Id, string Name, decimal Price);

