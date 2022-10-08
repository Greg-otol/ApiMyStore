namespace ApiMyStore.Endpoints.Categories;

public class CategoryById
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    //[Authorize(Policy = "EmployeePolicy")]
    [Authorize(Policy = "CpfPolicy")]
    public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
            return Results.NotFound();

        return Results.Ok(category);
    }
}
