using PersonalBlog.BLL.Dtos.Comments;

namespace PersonalBlog.BLL.Dtos.Posts;

public record PostDto(Guid Id, string Title, string Content, DateTime CreatedAt, IEnumerable<CommentDto> Comments);