using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Warehouse.UI;

internal static class Bootstrapper
{
    public static IServiceProvider Initialize()
    {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

    private static void ConfigureServices(this IServiceCollection services)
    {
            var configuration = services.UseConfiguration();

            services.AddInfrastructure();
        }

    private static IConfiguration UseConfiguration(this IServiceCollection services)
    {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            return configuration;
        }
}