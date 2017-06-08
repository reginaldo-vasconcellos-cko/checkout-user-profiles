using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using UserProfiles.Api.Services;

namespace UserProfiles.Security.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly IUserService _userService;

        private Dictionary<string, string> _apiKeys = new Dictionary<string, string>()
        {
            {"super_admin", "checkout.admin"},
            {"nike_admin", "nike.admin"},
            {"adidas_admin", "adidas.admin"},
            {"nike_shoes_user", "nikeshoes.user"},
            {"nike_accessories_user", "nikeacessories.user"},
            {"adidas_user", "adidas.user"},
            {"sales_user", "checkout.sales"}
        };

        public AuthMiddleware(RequestDelegate nextDelegate, 
            IUserService userService)
        {
            _nextDelegate = nextDelegate;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            StringValues key;
            string userName;

            if (!context.Request.Headers.TryGetValue("Authorization", out key)
                || !_apiKeys.TryGetValue(key.ToString(), out userName))
            {
                context.Response.StatusCode = 401;
                return;
            }

            var claims = await _userService.GetClaimsByUserNameAsync(userName);

            if (claims == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            claims.Add(new Claim(ClaimTypes.Name, userName));

            context.User = new ClaimsPrincipal(
                new ClaimsIdentity(claims)
            );

            await _nextDelegate.Invoke(context);
        }
    }
}
