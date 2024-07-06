using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Material.Colors;
using Material.Styles.Themes;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.UI.Views;

namespace Warehouse.UI;

public class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {       
        BindingPlugins.DataValidators.RemoveAt(0);

        var services = Bootstrapper.Initialize();
        var serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
        {
            desktopLifetime.MainWindow = new MainWindow(serviceProvider);
        }

        UseTheme();

        base.OnFrameworkInitializationCompleted();
    }

    private void UseTheme()
    {
        var primary = PrimaryColor.Red;
        var primaryColor = SwatchHelper.Lookup[(MaterialColor)primary];

        var secondary = SecondaryColor.Amber;
        var secondaryColor = SwatchHelper.Lookup[(MaterialColor)secondary];

        var theme = Theme.Create(Theme.Light, primaryColor, secondaryColor);
        var themeBootstrap = this.LocateMaterialTheme<MaterialThemeBase>();
        themeBootstrap.CurrentTheme = theme;
    }
}