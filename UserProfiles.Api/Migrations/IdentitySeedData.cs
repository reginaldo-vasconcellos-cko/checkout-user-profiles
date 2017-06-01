using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Repository;

namespace UserProfiles.Api.Migrations
{
    public class IdentitySeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<IdentityDbContext>();
            var userRepository = new UserRepository();

            string[] roles = { "SuperAdmin" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    var identityResult = await roleStore.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });

                    var claims = new List<Claim>
                    {
                        new Claim ("feature", "user.register"),
                        new Claim ("feature", "user.assignClaim"),
                        new Claim ("feature", "user.assignRole"),
                        new Claim ("feature", "user.assignResource"),
                        new Claim ("feature", "user.get"),
                        new Claim ("feature", "role.create"),
                        new Claim ("feature", "role.assignClaim"),
                        new Claim ("feature", "merchant.list"),
                        new Claim ("feature", "merchant.get"),
                        new Claim ("feature", "merchant.update"),
                        new Claim ("feature", "transaction.get"),
                        new Claim ("feature", "transaction.getByMerchant"),
                        new Claim ("feature", "transaction.getByBusiness")
                    };

                    var identityRole = context.Roles.FirstOrDefault(r => r.Name == role);

                    claims.ForEach(async c =>
                    {
                        await roleStore.AddClaimAsync(identityRole, c);
                    });
                }
            }

            var user = new IdentityUser
            {
                Email = "admin@checkout.com",
                NormalizedEmail = "ADMIN@CHECKOUT.COM",
                UserName = "admin@checkout.com",
                NormalizedUserName = "ADMIN@CHECKOUT.COM",
                PhoneNumber = "+923366633352",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var userStore = new UserStore<IdentityUser>(context);
                var result = await userStore.CreateAsync(user);

                var identityUser = await userStore.FindByEmailAsync(user.Email);
                var adminUserId = 1;
                
                userRepository.Update(new User { GuidRef = identityUser.Id, Id = adminUserId });
            }

            await AssignRolesAsync(serviceProvider, user.Email, roles);

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRolesAsync(IServiceProvider services, string email, string[] roles)
        {
            UserManager<IdentityUser> userManager = services.GetService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
