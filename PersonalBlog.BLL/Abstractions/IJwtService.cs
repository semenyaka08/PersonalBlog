using System.Security.Claims;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.BLL.Abstractions;

public interface IJwtService
{
    string GenerateToken(IEnumerable<Claim> claims);
}