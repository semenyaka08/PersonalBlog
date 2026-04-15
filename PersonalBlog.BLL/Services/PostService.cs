using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Dtos.Posts;
using PersonalBlog.BLL.Queries.Comments;
using PersonalBlog.BLL.Queries.Posts;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.BLL.Services;

public class PostService : IPostService
{
    public Task<IEnumerable<PostDto>> GetPostsAsync(GetAllPostsQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<PostDto?> GetPostAsync(GetPostByIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<PostDto> CreatePostAsync(CreatePostCommand command)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePostAsync(UpdatePostCommand command)
    {
        throw new NotImplementedException();
    }

    public Task DeletePostAsync(DeletePostCommand command)
    {
        throw new NotImplementedException();
    }

    private static Post CreatePost(string title, string content)
    {
        return new Post { Title = title, Content = content };
    }
}