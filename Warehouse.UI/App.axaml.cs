using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Material.Colors;
using Material.Styles.Themes;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.UI.ViewModels;
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
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
        {
            var serviceProvider = Bootstrapper.Initialize();
            desktopLifetime.MainWindow = serviceProvider.GetRequiredService<MainWindow>();
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