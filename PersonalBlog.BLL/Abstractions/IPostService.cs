using PersonalBlog.BLL.Dtos.Posts;
using PersonalBlog.BLL.Queries.Comments;
using PersonalBlog.BLL.Queries.Posts;

namespace PersonalBlog.BLL.Abstractions;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetPostsAsync(GetAllPostsQuery query);
    Task<PostDto?> GetPostAsync(GetPostByIdQuery query);

    Task<PostDto> CreatePostAsync(CreatePostCommand command);
    Task UpdatePostAsync(UpdatePostCommand command);
    Task DeletePostAsync(DeletePostCommand command);
}