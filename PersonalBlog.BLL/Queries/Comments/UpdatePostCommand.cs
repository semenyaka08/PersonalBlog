namespace PersonalBlog.BLL.Queries.Comments;

public record UpdatePostCommand(Guid Id, string Title, string Content);