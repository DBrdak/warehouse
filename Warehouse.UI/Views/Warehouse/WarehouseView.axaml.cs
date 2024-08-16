using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.Application.Transports.Models;
using Warehouse.UI.ViewModels.Warehouse;

namespace Warehouse.UI.Views.Warehouse;

public partial class WarehouseView : UserControl
{
    private readonly WarehouseViewModel _dataContext;

    public WarehouseView()
    {
        InitializeComponent();
    }

    public WarehouseView(MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = new WarehouseViewModel(mainWindow);
        _dataContext = (WarehouseViewModel)DataContext;
    }

    private void TransportDetails_Click(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button ?? new Button();
        var transport = button.DataContext as TransportModel;

        _dataContext.OpenTransportCommand.ExecuteAsync(transport?.Id ?? Guid.Empty);
    }
}