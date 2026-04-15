namespace PersonalBlog.DAL.Entities;

public class Comment
{
    public Guid Id { get; set; }
    
    public string Text { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid PostId { get; set; }
    
    public Post? Post { get; set; }
}