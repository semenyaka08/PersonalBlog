using PersonalBlog.DAL.Constants;

namespace PersonalBlog.BLL.Commands.Auth;

public record RegisterCommand(string Email, string Password, AppRoles Role = AppRoles.Visitor);
