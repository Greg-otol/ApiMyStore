namespace ApiMyStore.Endpoints.Products;

public class ProductDelete
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
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

        context.Remove(product);
        context.SaveChanges();

        return Results.Ok("Produto removido com sucesso!");
    }
}
