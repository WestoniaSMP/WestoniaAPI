
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System;
using WestoniaAPI.DataAccess.Context;
using WestoniaAPI.DataLayer.Entities.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using WestoniaAPI.DependencyInjection;

namespace WestoniaAPI
{
    public class Program
    {
//        Ich möchte in meiner.NET 8 ASP.NET WebAPI EntityFramework(ohne Fluent-API, mit Attributen) und ASP.NET Identity implementieren. Ich möchte zwei Identitäten haben:
//- MinecraftUser (Service-Secret[Umgebungsvariable] und Spieler-UUID zum Authentifizieren)
//- WebUser(E-Mail & Passwort oder Discord zum Authentifizieren)
//- MinecraftUser und WebUser können miteinander verbunden werden(WebUser hat eine Verlinkung zu MinecraftUser und anders herum.)

//UserManager und RoleManager sollen in eine Wrapper-Klasse UserLogic und RoleLogic gepackt werden.

//Ich habe die Top-Level-Statemes-Option für die Program.cs deaktiviert, das heisst ich habe eine Main-Methode.


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // Add services to the container.
            builder.Services.AddControllers();

            // Register AutoMapper profiles
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register Autofac modules
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new DataLogicModule());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
