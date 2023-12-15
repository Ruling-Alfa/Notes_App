using CrossCutting.Security.Configurations;
using CrossCutting.Persistance.SQL.Configurations;
using CRUD_API.Business;
using CRUD_API.Business.Interfaces;

namespace CRUD_API.Configurations
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.ConfigureSecurityInfra();
            services.ConfigurePersistanceInfra();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INoteService, NoteService>();
            return services;
        }
    }
}
