using PersonalBlog.DAL.Entities;

namespace PersonalBlog.DAL.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId);
    
    Task AddAsync(Comment comment);
    
    void Delete(Comment comment);
}