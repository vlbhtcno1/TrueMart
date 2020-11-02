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
using TrueMart.Application.DatabaseServices;
using TrueMart.Infrastructure.DatabaseServices;

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

            //Ensure database created
            using var conn = new SqlConnection(configuration.GetConnectionString("MasterConnection"));
            string databaseName = configuration.GetSection("DatabaseName").Value;
            var isDatabaseExists =
                conn.QueryFirstOrDefault<string>(
                    $"SELECT name FROM master.sys.databases WHERE name = N'{databaseName}'") != null;
            if (!isDatabaseExists)
            {
                conn.Execute($"Create database {databaseName}");
            }

            services.AddTransient<IDatabaseConnectionFactory>(e => new SqlConnectionFactory(configuration.GetConnectionString("TrueMartConnection")));

            //Add database service
            services.AddTransient<ICategoryService, CategoryService>();
            return services;
        }

        public static IApplicationBuilder UseFluentMigration(this IApplicationBuilder app)
        {

            using var scope = app.ApplicationServices.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator.MigrateUp();
            return app;
        }
    }
}
