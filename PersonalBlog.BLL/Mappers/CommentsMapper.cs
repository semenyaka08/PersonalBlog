using PersonalBlog.BLL.Dtos.Comments;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.BLL.Mappers;

public static class CommentsMapper
{
    public static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto(comment.Id, comment.Text, comment.CreatedAt);
    }
}