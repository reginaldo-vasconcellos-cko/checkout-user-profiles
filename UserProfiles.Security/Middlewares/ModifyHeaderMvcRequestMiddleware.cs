using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserProfiles.Security.Middlewares
{
    public class ModifyHeaderMvcRequestMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public ModifyHeaderMvcRequestMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.Add("Authorization", new[] { "super_admin" });

            await _nextDelegate.Invoke(context);
        }
    }
}
