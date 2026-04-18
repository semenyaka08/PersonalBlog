using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.DAL.DataSeeding;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Repositories;
using PersonalBlog.DAL.Repositories.Interfaces;
using PersonalBlog.DAL.UnitOfWork;

namespace PersonalBlog.DAL.DI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

        return services;
    }
}