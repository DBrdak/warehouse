using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.ViewModels.Management.Dialogs.Sectors;
using Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

namespace Warehouse.UI.Views.Management.Dialogs.Warehousemen;

public partial class EditWarehousemanDialog : Window
{
    private readonly WarehousemanModel _selectedWarehouseman;
    private readonly EditWarehousemanDialogModel _dataContext;

    public EditWarehousemanDialog(WarehousemanModel selectedWarehouseman)
    {
        InitializeComponent();
    }

    public EditWarehousemanDialog(
        WarehousemanModel selectedWarehouseman,
        MainWindow mainWindow,
        WarehousemenViewModel invoker)
    {
        InitializeComponent();
        _selectedWarehouseman = selectedWarehouseman;
        DataContext = new EditWarehousemanDialogModel(mainWindow, this, invoker);
        _dataContext = DataContext as EditWarehousemanDialogModel ??
                       throw new InvalidCastException(
                           $"Cannot convert type {DataContext.GetType().Name} to {nameof(EditWarehousemanDialogModel)}");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}