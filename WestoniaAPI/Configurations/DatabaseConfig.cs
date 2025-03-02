using Microsoft.EntityFrameworkCore;
using WestoniaAPI.DataAccess.Context;

namespace WestoniaAPI.Configurations
{
    /// <summary>
    /// Contains the configuration for Entity Framework Core.
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Adds the WestoniaDbContext to the service collection.
        /// In this case, the connection string is retrieved from the configuration, so it is important to have a valid connection string in the appsettings.json file.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns> The service collection with the WestoniaDbContext added. </returns>
        public static IServiceCollection AddWestoniaDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<WestoniaDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("WestoniaDb"));
            });

            return services;
        }
    }
}
