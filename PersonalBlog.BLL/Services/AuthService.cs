using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Commands.Auth;
using PersonalBlog.BLL.Constants;
using PersonalBlog.DAL.Constants;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.BLL.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public AuthService(IJwtService jwtService, UserManager<ApplicationUser> userManager)
    {
        _jwtService = jwtService;
        _userManager = userManager;
    }
    
    public async Task<AuthResponse> RegisterAsync(RegisterCommand command)
    {
        var appRole = GetAppRole(command.Role);   
        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException($"Registration failed: {errors}");
        }

        await _userManager.AddToRoleAsync(user, appRole.ToName());
        
        var claims = await BuildClaimsAsync(user);
        var token = _jwtService.GenerateToken(claims);
        
        return new AuthResponse(token, user.Email!);
    }

    public async Task<AuthResponse> LoginAsync(LoginCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, command.Password))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var claims = await BuildClaimsAsync(user);
        var token = _jwtService.GenerateToken(claims);
        
        return new AuthResponse(token, user.Email!);
    }
    
    private async Task<List<Claim>> BuildClaimsAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        return claims;
    }

    private static AppRoles GetAppRole(RegistrationRole role)
    {
        var appRole = role switch
        {
            RegistrationRole.Author => AppRoles.Author,
            RegistrationRole.Visitor => AppRoles.Visitor,
            _ => throw new ArgumentException("Invalid role.")
        };
        
        return appRole;
    }
}