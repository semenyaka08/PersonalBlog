using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Repositories.Interfaces;

namespace PersonalBlog.DAL.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _context;

    public PostRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        return await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Post post) => await _context.Posts.AddAsync(post);
    
    public void Update(Post post) => _context.Posts.Update(post);
    
    public void Delete(Post post) => _context.Posts.Remove(post);
}