using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.Application.Transports.Models;
using Warehouse.UI.Stores;
using Warehouse.UI.ViewModels.Warehouse;
using Warehouse.UI.Views.MainViews;

namespace Warehouse.UI.Views.Warehouse;

public partial class WarehouseView : UserControl
{
    private readonly WarehouseViewModel _dataContext;
    private readonly MainWindow _mainWindow;

    public WarehouseView()
    {
        InitializeComponent();
    }

    public WarehouseView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new WarehouseViewModel(_mainWindow);
        _dataContext = (WarehouseViewModel)DataContext;
    }

    private void TransportDetails_Click(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button ?? new Button();
        var transport = button.DataContext as TransportModel;

        _dataContext.OpenTransportCommand.ExecuteAsync(transport?.Id ?? Guid.Empty);
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = _mainWindow;

        mainWindow.ContentArea.Content = UserStore.CurrentUser == "admin" ?
            new MainDashboardView(_mainWindow) :
            new LogInView(_mainWindow);
    }
}