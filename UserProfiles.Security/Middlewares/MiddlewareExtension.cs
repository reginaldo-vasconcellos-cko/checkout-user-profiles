using Microsoft.AspNetCore.Builder;

namespace UserProfiles.Security.Middlewares
{
    #region ExtensionMethod

    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseModifyHeaderMvcRequestMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ModifyHeaderMvcRequestMiddleware>();
            return app;
        }
    }

    #endregion
}
