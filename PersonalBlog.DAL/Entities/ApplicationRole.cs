using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.DAL.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() : base() { }
    
    public ApplicationRole(string roleName) : base(roleName) { }
}