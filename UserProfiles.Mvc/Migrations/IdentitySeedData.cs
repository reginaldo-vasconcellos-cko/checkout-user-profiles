using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Data.Repository;

namespace UserProfiles.Mvc.Migrations
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
                            new Claim ("feature", "transaction.list"),
                            new Claim ("feature", "transaction.getByMerchant"),
                            new Claim ("feature", "transaction.getByBusiness"),
                            new Claim ("feature", "identity.get"),
                            new Claim ("feature", "identity.getRoles"),
                            new Claim ("feature", "identity.getPermissions"),
                            new Claim ("feature", "claim.list"),
                            new Claim ("feature", "claim.details"),
                            new Claim ("feature", "claim.create"),
                            new Claim ("feature", "claim.edit"),
                            new Claim ("feature", "role.list"),
                            new Claim ("feature", "role.details"),
                            new Claim ("feature", "role.create"),
                            new Claim ("feature", "role.edit"),
                            new Claim ("feature", "user.list"),
                            new Claim ("feature", "user.details"),
                            new Claim ("feature", "user.create"),
                            new Claim ("feature", "user.edit"),
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
                UserName = "checkout.admin",
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
    //public static async Task InitializeAsync(IServiceProvider serviceProvider)
    //{
    //    var userService = serviceProvider.GetService<IUserService>();
    //    var roleService = serviceProvider.GetService<IRoleService>();
    //    var claimService = serviceProvider.GetService<IClaimService>();
    //    var resourceService = serviceProvider.GetService<IResourceIdentityService>();

    //    var claims = await claimService.ListAsync();
    //    var adminRole = "SuperAdmin";

    //    await roleService.CreateAsync(new RoleDto
    //    {
    //         Name = adminRole,
    //         Claims = claims.ToList()
    //    });

    //    var resources = await resourceService.ListAsync();

    //    var userDto = new CreateAccountRequest
    //    {
    //        Name = "super.admin",
    //        Email = "admin@checkout.com",
    //        Roles = new[] { adminRole },
    //        Resources = resources.Select(c => c.Id).ToArray()
    //    };

    //    await userService.CreateAsync(userDto);
    //}
}
