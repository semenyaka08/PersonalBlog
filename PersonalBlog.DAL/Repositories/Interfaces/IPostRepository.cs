using PersonalBlog.DAL.Entities;

namespace PersonalBlog.DAL.Repositories.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Post>> GetAllAsync();

    Task<Guid> AddAsync(Post post);
    
    void Update(Post post);
    
    void Delete(Post post);
}