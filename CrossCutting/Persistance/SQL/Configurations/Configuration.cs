using CrossCutting.Persistance.SQL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Persistance.SQL.Configurations
{
    public static class Configuration
    {
        public static IServiceCollection ConfigurePersistanceInfra(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericUnitOfWork), typeof(GenericUnitOfWork));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
