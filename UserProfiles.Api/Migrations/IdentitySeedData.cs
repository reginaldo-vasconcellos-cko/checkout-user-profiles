using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UserProfiles.Api.Migrations
{
    public class IdentitySeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<IdentityDbContext>();

            string[] roles = { "ADMINISTRATORT" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    var claims = new List<IdentityRoleClaim<string>>
                    {
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "user.register"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "user.assignClaim"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "user.assignRole"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "user.assignResource"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "user.get"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "merchant.list"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "merchant.get"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "merchant.update"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "transaction.get"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "transaction.getByMerchant"},
                        new IdentityRoleClaim<string> {ClaimType = "feature", ClaimValue = "transaction.getByBusiness"},
                    };

                    var identityResult = await roleStore.CreateAsync(new IdentityRole{ Name = role, NormalizedName = role.ToUpper() });
                }
            }

            var user = new IdentityUser
            {
                Email = "admin12345@checkout.com",
                NormalizedEmail = "ADMIN12345@CHECKOUT.COM",
                UserName = "admin12345@checkout.com",
                NormalizedUserName = "ADMIN12345@CHECKOUT.COM",
                PhoneNumber = "+923366633352",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var userStore = new UserStore<IdentityUser>(context);
                var result = await userStore.CreateAsync(user);
            }

            AssignRoles(serviceProvider, user.Email, roles);

            context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            RoleManager<IdentityRole> roleManager = services.GetService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> _userManager = services.GetService<UserManager<IdentityUser>>();

            IdentityUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
