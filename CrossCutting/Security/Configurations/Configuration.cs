using CrossCutting.Security.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Security.Configurations
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureSecurityInfra(this IServiceCollection services)
        {
            services.AddSingleton<ITokenHelper, TokenHelper>();
            services.AddSingleton<IHasher, HMACSHA256_Hasher>();

            return services;
        }
    }
}
