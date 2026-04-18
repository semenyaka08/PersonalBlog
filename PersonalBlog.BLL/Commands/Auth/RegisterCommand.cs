using PersonalBlog.BLL.Constants;
using PersonalBlog.DAL.Constants;

namespace PersonalBlog.BLL.Commands.Auth;

public record RegisterCommand(string Email, string Password, RegistrationRole Role = RegistrationRole.Visitor);
