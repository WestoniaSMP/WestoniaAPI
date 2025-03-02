namespace WestoniaAPI.Configurations
{
    /// <summary>
    /// Contains the configuration for the Swagger UI.
    /// </summary>
    public static class SwaggerUIConfig
    {
        /// <summary>
        /// Adds the Swagger UI to the WebApplication, sets the versioning endpoints, and adds the Discord OAuth2 configuration.
        /// </summary>
        /// <param name="app"> The WebApplication to configure. </param>
        /// <param name="config"> The configuration to use. </param>
        /// <returns> The WebApplication with the Swagger UI configured. </returns>
        public static WebApplication UseWestoniaSwaggerUI(this WebApplication app, IConfiguration config)
        {
            var discordClientId = config["DiscordAuth:ClientId"] ?? string.Empty;
            var discordClientSecret = config["DiscordAuth:ClientSecret"] ?? string.Empty;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Westonia API v1");
                //c.OAuthClientId(discordClientId);
                //c.OAuthClientSecret(discordClientSecret);
                //c.OAuthUsePkce();
                //c.OAuthScopes("identify", "email");
            });

            return app;
        }
    }
}
