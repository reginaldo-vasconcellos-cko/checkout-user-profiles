using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using UserProfiles.Api.Services;

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
