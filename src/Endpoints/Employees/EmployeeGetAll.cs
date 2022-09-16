using Microsoft.AspNetCore.Identity;

namespace ApiMyStore.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(UserManager<IdentityUser> userManager)
    {
        var users = userManager.Users.ToList();
        var employees = new List<EmployeeResponse>();
        foreach (var item in users)
        {
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimCode = claims.FirstOrDefault(c => c.Type == "EmployeeCode");
            var userCode = claimCode != null ? claimCode.Value : string.Empty;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(userCode, userName, item.Email));
        }
        return Results.Ok(employees);
    }
}
