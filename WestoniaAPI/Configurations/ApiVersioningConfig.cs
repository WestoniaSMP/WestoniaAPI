using Asp.Versioning;
using Microsoft.Extensions.Options;

namespace WestoniaAPI.Configurations
{
    /// <summary>
    /// Contains the configuration for the API versioning.
    /// </summary>
    public static class ApiVersioningConfig
    {
        /// <summary>
        /// Configures the API versioning options.
        /// </summary>
        /// <param name="services"> The ServiceCollection to configure. </param>
        /// <returns> The ServiceCollection with the API versioning configured. </returns>
        public static IServiceCollection AddWestoniaApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                // Shows possible versions in the header
                options.ReportApiVersions = true;

                // Allow multiple ways to specify the version (URL [/api/v1/], Header [x-api-version], Media Type [v=1])
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("v")
                );
            });

            return services;
        }
    }
}
