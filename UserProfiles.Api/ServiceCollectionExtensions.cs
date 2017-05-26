using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using UserProfiles.Api.Repository;
using UserProfiles.Api.Security.Handlers;

namespace UserProfiles.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddTransient<IMerchantRepository, MerchantRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserResouceIdentityRepository, UserResouceIdentityRepository>();

            services.AddSingleton<IAuthorizationHandler, ResourceAccessHandler>();

            return services;
        }
    }
}
