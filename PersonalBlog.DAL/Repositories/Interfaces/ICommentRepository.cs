using PersonalBlog.DAL.Entities;

namespace PersonalBlog.DAL.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId);

    Task<Guid> AddAsync(Comment comment);
    
    void Delete(Comment comment);
    
    Task<Comment?> GetByIdAsync(Guid id);
}