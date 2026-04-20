using PersonalBlog.BLL.Dtos.Posts;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.BLL.Mappers;

public static class PostsMapper
{
    public static PostDto ToDto(this Post post)
    {
        return new PostDto(post.Id, post.Title, post.Content, post.CreatedAt, post.Comments.Select(z => z.ToDto()));
    }
}