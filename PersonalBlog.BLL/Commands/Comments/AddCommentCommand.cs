namespace PersonalBlog.BLL.Commands.Comments;

public record AddCommentCommand(Guid PostId, string Text);