using CrossCutting.Persistance.SQL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Data.Configurations
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureRepositorySettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigurePersistanceInfra();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationContext))));

            services.AddTransient<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddScoped<DbContext, ApplicationContext>();
            return services;
        }

        public static IApplicationBuilder CreateDB(
            this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            using (var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
                dbcontext.Database.EnsureCreated();

            return app;
        }
    }
}
