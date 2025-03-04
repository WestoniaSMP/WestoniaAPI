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
            string? connectionString = config.GetConnectionString("MariaDb") ?? throw new Exception("MariaDb connection string not found");

            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<WestoniaDbContext>(options =>
            {
                options.UseMySql(config.GetConnectionString("MariaDb"), serverVersion);
            });

            return services;
        }
    }
}
