using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.UI.Stores;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.Views.MainViews;

namespace Warehouse.UI.Views.Management;

public partial class ManagementView : UserControl
{
    private readonly MainWindow _mainWindow;

    public ManagementView()
    {
        InitializeComponent();
    }

    public ManagementView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new ManagementViewModel(mainWindow);
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = _mainWindow;

        mainWindow.ContentArea.Content = UserStore.CurrentUser == "admin" ?
            new MainDashboardView(_mainWindow) :
            new LogInView(_mainWindow);
    }
}