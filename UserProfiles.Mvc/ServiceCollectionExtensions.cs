using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using UserProfiles.Api.Services;
using UserProfiles.Data.Repository;

namespace UserProfiles.Mvc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            //services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IClaimService, ClaimService>();
            services.AddTransient<IResourceIdentityService, ResourceIdentityService>();
            services.AddTransient<IUserResourceIdentityService, UserResourceIdentityService>();

            //repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddTransient<IResourceIdentityRepository, ResourceIdentityRepository>();
            services.AddTransient<IUserResourceIdentityRepository, UserResourceIdentityRepository>();

            return services;
        }
    }
}
