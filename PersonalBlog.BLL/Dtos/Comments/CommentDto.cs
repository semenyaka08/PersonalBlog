namespace PersonalBlog.BLL.Dtos.Comments;

public record CommentDto(Guid Id, string Text, DateTime CreatedAt);