using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using WestoniaAPI.DataLayer.Entities.Security;
using WestoniaAPI.DataLogic;
using WestoniaAPI.Core;
using System.Security.Cryptography;

namespace WestoniaAPI.Configurations
{
    /// <summary>
    /// Configuration class for Westonia authentication services.
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// Adds Westonia authentication services, including Discord OAuth2, JWT bearer and cookie authentication. <br/>
        /// <br/><br/>
        /// For Discord OAuth2, the following configuration keys are required: <br/>
        /// - DiscordAuth:ClientId <br/>
        /// - DiscordAuth:ClientSecret <br/>
        /// - DiscordAuth:RedirectUri <br/>
        /// <br/><br/>
        /// For JWT bearer authentication, the following configuration keys are required: <br/>
        /// - JwtSettings:Key <br/>
        /// - JwtSettings:Issuer <br/>
        /// - JwtSettings:Audience <br/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns> The service collection with the authentication services added. </returns>
        public static IServiceCollection AddWestoniaAuthenticationServices(this IServiceCollection services, IConfiguration config)
        {
            string discordClientId = config["DiscordAuth:ClientId"] ?? string.Empty;
            string discordClientSecret = config["DiscordAuth:ClientSecret"] ?? string.Empty;
            string discordRedirectUri = config["DiscordAuth:RedirectUri"] ?? string.Empty;
            string discordScopes = config["DiscordAuth:Scopes"] ?? string.Empty;

            byte[] derivedKey = new byte[32];

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] jwtEncryptionKeyBytes = Encoding.UTF8.GetBytes(config["JwtSettings:EncryptionKey"] ?? throw new Exception("JWT key not found"));
                derivedKey = sha256.ComputeHash(jwtEncryptionKeyBytes);
            }

            string jwtIssuer = config["JwtSettings:Issuer"] ?? string.Empty;
            string jwtAudience = config["JwtSettings:Audience"] ?? string.Empty;

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(derivedKey)
                };
            })
            .AddOAuth("Discord", options =>
            {
                options.AuthorizationEndpoint = "https://discord.com/api/oauth2/authorize";
                if (discordScopes.Contains(','))
                {
                    foreach (var scope in discordScopes.Split(','))
                    {
                        options.Scope.Add(scope);
                    }
                } else
                {
                    options.Scope.Add(discordScopes);
                }

                options.CallbackPath = new PathString(discordRedirectUri); // !!! CallbackPath = RedirectUri !!!

                options.ClientId = discordClientId;
                options.ClientSecret = discordClientSecret;

                options.TokenEndpoint = "https://discord.com/api/oauth2/token";
                options.UserInformationEndpoint = "https://discord.com/api/users/@me";

                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");

                options.AccessDeniedPath = "/api/v1/auth/AccessDenied";

                options.SaveTokens = true;


                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                        context.RunClaimActions(user.RootElement);

                        ClaimsIdentity? identity = context.Identity as ClaimsIdentity;

                        if (identity is null)
                        {
                            context.Fail("Failed to retrieve user information from Discord");
                            return;
                        }

                        IUserLogic userLogic = context.HttpContext.RequestServices.GetRequiredService<IUserLogic>();
                        bool success = await CreateUserIfNotExists(userLogic, identity);

                        if (!success)
                        {
                            context.Fail("User creation failed");
                            return;
                        }

                        context.Success();
                    }
                };
            });
            return services;
        }

        private static async Task<bool> CreateUserIfNotExists(IUserLogic userLogic, ClaimsIdentity identity)
        {
            var discordIdClaim = identity.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
            var discordNameClaim = identity.FindFirst(claim => claim.Type == ClaimTypes.Name);

            if (discordIdClaim is null || discordNameClaim is null)
            {
                return false;
            }

            string discordId = discordIdClaim.Value;
            string discordName = discordNameClaim.Value;

            if (!(await userLogic.UserExists(discordId)))
            {
                IdentityResult result = await userLogic.CreateUser(discordId, discordName);
                return result.Succeeded;
            }

            return true;
        }
    }
}
