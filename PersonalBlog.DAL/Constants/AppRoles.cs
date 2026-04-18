namespace PersonalBlog.DAL.Constants;

public enum AppRoles
{
    Admin,
    Author,
    Visitor
}

public static class AppRolesExtensions
{
    public static string ToName(this AppRoles role) => role.ToString();
}