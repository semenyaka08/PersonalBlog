using PersonalBlog.BLL.Commands.Comments;
using PersonalBlog.BLL.Dtos.Comments;

namespace PersonalBlog.BLL.Abstractions;

public interface ICommentService
{
    Task<Guid> AddCommentAsync(AddCommentCommand command);
    
    Task DeleteCommentAsync(DeleteCommentCommand command);
}