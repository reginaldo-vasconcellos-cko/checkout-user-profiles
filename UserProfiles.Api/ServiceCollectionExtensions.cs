using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using UserProfiles.Api.Repository;
using UserProfiles.Api.Security.Handlers;
using UserProfiles.Api.Services;

namespace UserProfiles.Api
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
            services.AddSingleton<IMerchantService, MerchantService>();

            //repositories
            services.AddTransient<IMerchantRepository, MerchantRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserResouceIdentityRepository, UserResouceIdentityRepository>();

            //others
            services.AddSingleton<IAuthorizationHandler, ResourceAccessHandler>();

            return services;
        }
    }
}
