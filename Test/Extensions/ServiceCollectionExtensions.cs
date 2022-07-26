using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using DataAccess.Repositories;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Models.Configuration;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Contracts.Engine;
using Engine;
using FluentValidation;
using Models;
using Engine.Validators;

namespace Test.Extensions
{
    [ExcludeFromCodeCoverage]
    public  static class ServiceCollectionExtensions
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddTransient<IEmailRepository, EmailRepository>();
        }

        public static void RegisterDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(ConnectionStringSettings.KEY).Get<ConnectionStringSettings>();
            services.AddDbContext<EmailContext>(options => options.UseSqlServer(settings.DefaultConnectionString));
        }

        public static void RegisterValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Email>, EmailValidator>();
        }

        public static void RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.KEY));
        }
        public static void RegisterEngines(this IServiceCollection services)
        {
            services.AddScoped<IEngineSender, EngineSender>();
        }
    }
}
