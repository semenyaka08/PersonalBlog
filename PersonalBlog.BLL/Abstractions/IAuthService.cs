using PersonalBlog.BLL.Commands.Auth;

namespace PersonalBlog.BLL.Abstractions;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterCommand command);
    
    Task<AuthResponse> LoginAsync(LoginCommand command);
}

public record AuthResponse(string Token, string Email);