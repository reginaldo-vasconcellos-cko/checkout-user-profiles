using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Security.Middlewares
{
    public class AuthMiddleware
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _loginManager;

        private readonly RequestDelegate _nextDelegate;

        private const string PASSWORD = "Admin123$";

        private Dictionary<string, string> _apiKeys = new Dictionary<string, string>()
        {
            {"admin", "admin@checkout.com"},
            {"adidas_user", "user@adidas.com"},
            {"adidas_am", "account.manager1@adidas.com"}
        };

        public AuthMiddleware(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, RequestDelegate nextDelegate, SignInManager<IdentityUser> loginManager)
        {
            _nextDelegate = nextDelegate;
            _userManager = userManager;
            _roleManager = roleManager;
            _loginManager = loginManager;
        }

        public async Task<Task> Invoke(HttpContext context)
        {
            StringValues key;
            string user;
            if (!context.Request.Headers.TryGetValue("Authorization", out key)
                || !_apiKeys.TryGetValue(key.ToString(), out user))
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            }

            var userIdentity = await _userManager.FindByNameAsync(user);
            var roleNames = await _userManager.GetRolesAsync(userIdentity);

            var roles = new List<IdentityRole>();

            foreach (var roleName in roleNames)
                roles.Add(await _roleManager.FindByNameAsync(roleName));

            var claims = new List<Claim>{new Claim(ClaimTypes.Name, user)};

            foreach (var role in roles)
                claims.AddRange(await _roleManager.GetClaimsAsync(role));

            context.User = new ClaimsPrincipal(
                new ClaimsIdentity(claims)
            );

            return _nextDelegate.Invoke(context);
        }
    }
}
