using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.UI.Stores;
using Warehouse.UI.ViewModels.MainDashboard;

namespace Warehouse.UI.Views.MainViews;

public partial class MainDashboardView : UserControl
{
    private readonly MainWindow _mainWindow;

    public MainDashboardView()
    {
        InitializeComponent();
    }

    public MainDashboardView(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        InitializeComponent();
        DataContext = new MainDashboardViewModel(mainWindow);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = _mainWindow;

        mainWindow.ContentArea.Content = new LogInView(_mainWindow);
    }
}