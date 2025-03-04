using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WestoniaAPI.DataAccess.Context;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.Configurations
{
    /// <summary>
    /// Contains the configuration for the Identity framework.
    /// </summary>
    public static class IdentityConfig
    {
        /// <summary>
        /// Adds the Identity framework to the service collection, using the WestoniaDbContext and the WestoniaUser and WestoniaRole entities.
        /// </summary>
        /// <param name="services"> The ServiceCollection to add the Identity framework to. </param>
        /// <returns> The ServiceCollection with the Identity framework added. </returns>
        public static IServiceCollection AddWestoniaIdentity(this IServiceCollection services)
        {
            services.AddIdentity<WestoniaUser, WestoniaRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<WestoniaDbContext>().AddDefaultTokenProviders();
            
            return services;
        }
    }
}
