using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PersonalBlog.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userIdStr = principal.FindFirstValue(JwtRegisteredClaimNames.Sub) 
                        ?? principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
        {
            throw new UnauthorizedAccessException("User ID not found in token.");
        }

        return userId;
    }
}