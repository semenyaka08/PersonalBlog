using PersonalBlog.BLL.Constants;
using PersonalBlog.DAL.Constants;

namespace PersonalBlog.API.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppPolicies.CanWriteComments, policy =>
                policy.RequireRole(
                    AppRoles.Admin.ToName(),
                    AppRoles.Author.ToName(),
                    AppRoles.Visitor.ToName()
                ));

            options.AddPolicy(AppPolicies.CanWritePosts, policy =>
                policy.RequireRole(
                    AppRoles.Admin.ToName(),
                    AppRoles.Author.ToName()
                ));
        });

        return services;
    }
}