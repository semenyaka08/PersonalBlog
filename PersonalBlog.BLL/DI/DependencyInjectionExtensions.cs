using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Services;

namespace PersonalBlog.BLL.DI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}