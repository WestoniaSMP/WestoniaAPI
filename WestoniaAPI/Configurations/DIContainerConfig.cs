using Autofac;
using Microsoft.AspNetCore.Identity;
using WestoniaAPI.DataAccess.Context;
using WestoniaAPI.DataLayer.Entities.Security;
using WestoniaAPI.DependencyInjection;

namespace WestoniaAPI.Configurations
{
    /// <summary>
    /// Contains the configuration for the Autofac Dependency Injection containers.
    /// </summary>
    public static class DIContainerConfig
    {
        /// <summary>
        /// Registers the available Westonia modules with the Autofac container.
        /// </summary>
        /// <param name="host"> The HostBuilder to configure. </param>
        /// <param name="configuration"> The configuration to register with the container. </param>
        /// <returns> The HostBuilder with the Westonia modules registered. </returns>
        public static IHostBuilder AddWestoniaDIContainers(this IHostBuilder host, IConfiguration configuration)
        {
            host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();

                builder.RegisterModule(new DataLogicModule());
            });

            return host;
        }
    }
}
