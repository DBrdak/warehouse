using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Warehouse.UI;

internal sealed class Program
{
    [STAThread]
    public static async Task Main(string[] args)
    {
        var app = BuildAvaloniaApp();
        var serviceProvider = Bootstrapper.Initialize();
        
        app.StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}