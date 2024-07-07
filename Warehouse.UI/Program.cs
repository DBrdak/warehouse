using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.UI.Observers;

namespace Warehouse.UI;

internal sealed class Program
{
    [STAThread]
    public static async Task Main(string[] args)
    {
        var app = BuildAvaloniaApp();
        var serviceProvider = Bootstrapper.Initialize();
        RxApp.DefaultExceptionHandler = new ExceptionHandler();
        app.StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}