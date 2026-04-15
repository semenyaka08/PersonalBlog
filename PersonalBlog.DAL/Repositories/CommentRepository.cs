using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Repositories.Interfaces;

namespace PersonalBlog.DAL.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly BlogDbContext _context;

    public CommentRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId)
    {
        return await _context.Comments
            .AsNoTracking()
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }

    public async Task<Guid> AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);

        return comment.Id;
    }
    
    public void Delete(Comment comment) => _context.Comments.Remove(comment);
    
    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        
        return comment;
    }
}