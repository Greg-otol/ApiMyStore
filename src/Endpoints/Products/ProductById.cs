namespace ApiMyStore.Endpoints.Products;

public class ProductById
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    //[Authorize(Policy = "EmployeePolicy")]
    [Authorize(Policy = "CpfPolicy")]
    public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var product = context.Products.Where(p => p.Id == id).FirstOrDefault();

        if (product == null)
            return Results.NotFound();

        if (!product.IsValid)
            return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());

        return Results.Ok(product);
    }
}
