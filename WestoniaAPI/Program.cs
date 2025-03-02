
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WestoniaAPI.Configurations;
using WestoniaAPI.DataAccess.Context;
using WestoniaAPI.DataLayer.Entities.Security;
using WestoniaAPI.DependencyInjection;

namespace WestoniaAPI
{
    /// <summary>
    /// Main class of the WestoniaAPI project. Self-explanatory.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Creates the WebApplication and configures the services and middleware.
        /// Obvously, this is the main entry point of the application, but Visual Studio doesn't like it when you dont document it (CS1591).
        /// </summary>
        /// <param name="args"> The command line arguments. (currently unused) </param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // Add services to the container.
            builder.Services.AddControllers();

            // Register AutoMapper profiles
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register DbContext
            builder.Services.AddWestoniaDbContext(builder.Configuration);

            // Register Identity
            builder.Services.AddWestoniaIdentity();

            // Register Autofac modules
            builder.Host.AddWestoniaDIContainers(builder.Configuration);

            // Configure authentication
            builder.Services.AddWestoniaAuthenticationServices(builder.Configuration);

            // Set static URLs for http and https
            builder.WebHost.UseUrls("http://*:8080", "https://*:8081");

            // Add Westonia API versioning
            builder.Services.AddWestoniaApiVersioning();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Add SwaggerGen to the services and configure it
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<SwaggerConfig>();

            var app = builder.Build();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Development-only middleware here
            }

            // Add Swagger/SwaggerUI middleware
            app.UseWestoniaSwaggerUI(builder.Configuration);

            app.UseHttpsRedirection();
            app.UseRouting();

            // CORS Configuration
            app.UseCors(options =>
            {
                options.WithOrigins("https://discord.com", "https://localhost:8081");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.WithExposedHeaders();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
