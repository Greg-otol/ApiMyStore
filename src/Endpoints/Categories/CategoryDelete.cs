namespace ApiMyStore.Endpoints.Categories;

public class CategoryDelete
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    //[Authorize(Policy = "EmployeePolicy")]
    [Authorize(Policy = "CpfPolicy")]
    public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
            return Results.NotFound();

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.Remove(category);
        context.SaveChanges();

        return Results.Ok("Categoria removida com sucesso!");
    }
}
