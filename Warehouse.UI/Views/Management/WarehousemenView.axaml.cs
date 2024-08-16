using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.UI.ViewModels.Management;

namespace Warehouse.UI.Views.Management;

public partial class WarehousemenView : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly WarehousemenViewModel _dataContext;

    public bool IsSelected(Guid selectedId) => _dataContext.SelectedWarehouseman?.Id == selectedId;

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

    private void SelectWarehouseman(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        _dataContext.SelectedWarehouseman = button?.DataContext as WarehousemanModel;
        _dataContext.IsWarehousemanSelected = true;
    }
}