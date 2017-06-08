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
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<IUserResourceIdentityService, UserResourceIdentityService>();
            
            //repositories
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserResourceIdentityRepository, UserResourceIdentityRepository>();

            //others
            services.AddSingleton<IAuthorizationHandler, ResourceAccessHandler>();

            return services;
        }
    }
}
