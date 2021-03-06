﻿using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluentMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var serviceProvider = CreateServices();


            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString("Server=.;Database=mrAmiriSupermarket;Trusted_Connection=True;")// should create the database first manually
                    .ScanIn(typeof(Program).Assembly).For.Migrations())// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly?view=net-5.0
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
