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
            {"super_admin", "admin@checkout.com"},
            {"nike_admin", "admin@nike.com"},
            {"adidas_admin", "admin@adidas.com"},
            {"nike_shoes_user", "user-shoes@nike.com"},
            {"nike_accessories_user", "user-accessories@nike.com"},
            {"adidas_user", "user@adidas.com"},
            {"sales_user", "sales@checkout.com"},
            {"test_user", "finaltestuser"}
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

            claims.Add(new Claim(ClaimTypes.Name, userName));

            context.User = new ClaimsPrincipal(
                new ClaimsIdentity(claims)
            );

            await _nextDelegate.Invoke(context);
        }
    }
}
