using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using UserProfiles.Api.Services;
using UserProfiles.Data.Repository;
using UserProfiles.Security.Handlers;

namespace UserProfiles.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            //services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<IClaimService, ClaimService>();
            services.AddSingleton<IMerchantService, MerchantService>();
            services.AddTransient<IUserResourceIdentityService, UserResourceIdentityService>();
            services.AddTransient<IUserResourceIdentityService, UserResourceIdentityService>();

            //repositories
            services.AddTransient<IMerchantRepository, MerchantRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddTransient<IUserResourceIdentityRepository, UserResourceIdentityRepository>();
            services.AddTransient<IResourceIdentityRepository, ResourceIdentityRepository>();

            //others
            services.AddSingleton<IAuthorizationHandler, ResourceAccessHandler>();

            return services;
        }
    }
}
