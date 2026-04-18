using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.DAL.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}