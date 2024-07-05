using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Warehouse.Application;
using Warehouse.Infrastructure;
using Warehouse.UI.ViewModels.LogIn;
using Warehouse.UI.ViewModels.MainDashboard;
using Warehouse.UI.Views;

namespace Warehouse.UI;

internal static class Bootstrapper
{
    public static IServiceProvider Initialize() =>
        new ServiceCollection()
            .ConfigureServices()
            .ConfigureViews()
            .ConfigureViewModels()
            .BuildServiceProvider();

    private static IServiceCollection ConfigureServices(this IServiceCollection services) =>
        services.UseConfiguration(out var configuration)
            .AddInfrastructure()
            .AddApplication();

    private static IServiceCollection ConfigureViews(this IServiceCollection services) =>
        services.AddTransient<MainWindow>()
            .AddTransient<LogInView>()
            .AddTransient<MainDashboardView>();

    private static IServiceCollection ConfigureViewModels(this IServiceCollection services) =>
        services.AddTransient<LoginViewModel>()
            .AddTransient<MainDashboardViewModel>();

    private static IServiceCollection UseConfiguration(
        this IServiceCollection services,
        out IConfiguration configuration)
    {
        configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .Build();

        services.AddSingleton(configuration);

        return services;
    }
}