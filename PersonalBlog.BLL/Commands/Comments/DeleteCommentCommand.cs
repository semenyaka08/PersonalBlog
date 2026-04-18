namespace PersonalBlog.BLL.Commands.Comments;

public record DeleteCommentCommand(Guid Id, Guid CurrentUserId);