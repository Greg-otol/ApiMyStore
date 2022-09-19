namespace ApiMyStore.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/orders/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(Guid Id, HttpContext http, ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        var clientClaim = http.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier);
        var employeeClaim = http.User.Claims
            .FirstOrDefault(c => c.Type == "EmployeeCode");
        var nameClaim = http.User.Claims
            .FirstOrDefault(c => c.Type == "Name");

        var order = context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == Id);

        if (order.ClientId != clientClaim.Value && employeeClaim == null)
            return Results.Forbid();

        var client = await userManager.FindByIdAsync(order.ClientId);

        var productsResponse = order.Products.Select(p => new OrderProduct(p.Id, p.Name, p.Price));
        var orderResponse = new OrderResponse(order.Id, nameClaim.Value, client.Email, productsResponse, order.Total, order.DeliveryAddress);
        
        return Results.Ok(orderResponse);
    }
}
