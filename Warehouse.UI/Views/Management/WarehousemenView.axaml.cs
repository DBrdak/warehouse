using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.UI.ViewModels.Management;

namespace Warehouse.UI.Views.Management;

public partial class WarehousemenView : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly WarehousemenViewModel _dataContext;

    public WarehousemenView()
    {
        InitializeComponent();
        _mainWindow = new MainWindow();
        _dataContext = new WarehousemenViewModel(_mainWindow);
    }

    public WarehousemenView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new WarehousemenViewModel(mainWindow);
        _dataContext = DataContext as WarehousemenViewModel ?? new WarehousemenViewModel(mainWindow);
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        await _dataContext.FetchWarehousemenAsync();
    }
}