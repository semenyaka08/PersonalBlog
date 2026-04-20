namespace PersonalBlog.DAL.Entities;

public class Post
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid AuthorId { get; set; }

    public ApplicationUser? Author { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}