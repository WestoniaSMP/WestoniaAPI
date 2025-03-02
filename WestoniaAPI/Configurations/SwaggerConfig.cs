using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WestoniaAPI.Configurations
{
    /// <summary>
    /// Contains the configuration for Swagger documentation.
    /// </summary>
    public class SwaggerConfig : IConfigureOptions<SwaggerGenOptions>
    {
        /// <summary>
        /// Sets up the Swagger documentation options.
        /// </summary>
        /// <param name="options"> The SwaggerGenOptions to configure. </param>
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WestoniaAPI",
                Version = "v1",
                Description = "The official API for the Westonia project. For more information, visit the [GitHub repository](https://github.com/WestoniaSMP/WestoniaAPI). Authorize via Discord OAuth2 to see the full API documentation.",
                Contact = new OpenApiContact
                {
                    Name = "Westonia Development Team",
                },
                License = new OpenApiLicense
                {
                    Name = "GPLv3",
                    Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.html")
                },
                Extensions = new Dictionary<string, IOpenApiExtension>
                {
                    { "x-logo", new OpenApiObject
                        {
                            { "url", new OpenApiString("https://cdn.discordapp.com/icons/1014116834670301225/94ad17a061eb2fb90dc98c00e2de3c10.webp") },
                            { "backgroundColor", new OpenApiString("#000000") },
                            { "altText", new OpenApiString("Westonia Logo") }
                        }
                    }
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}
