namespace ApiMyStore.Endpoints.Products;

public class ProductPut
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    //[Authorize(Policy = "EmployeePolicy")]
    [Authorize(Policy = "CpfPolicy")]
    public static IResult Action([FromRoute] Guid id, ProductRequest productRequest, HttpContext http, ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var product = context.Products.Where(p => p.Id == id).FirstOrDefault();
        
        if (product == null)
            return Results.NotFound();
        
        product.EditInfo(productRequest.Name, productRequest.Description, productRequest.Price, productRequest.ImageUrl, productRequest.HasStock, productRequest.Active, userId);

        if (!product.IsValid)
            return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());

        context.SaveChanges();

        return Results.Ok(product.Id);
    }
}
