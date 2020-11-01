using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TrueMart.Infrastructure
{
    public static class RegisterServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(config =>
            {
                config.AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("TrueMartConnection"))
                    .ScanIn(Assembly.GetExecutingAssembly())
                    .For.All();
            }).AddLogging(cfg => cfg.AddFluentMigratorConsole());
            return services;
        }

        public static IApplicationBuilder UseFluentMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator.ListMigrations();
            migrator.MigrateUp();
            return app;
        }
    }
}
