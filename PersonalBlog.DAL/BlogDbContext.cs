using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.DAL;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
}