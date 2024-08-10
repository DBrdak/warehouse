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
    private readonly EditWarehousemanDialogModel _dataContext;

    public EditWarehousemanDialog()
    {
        InitializeComponent();
    }

    public EditWarehousemanDialog(
        MainWindow mainWindow,
        WarehousemenViewModel invoker)
    {
        InitializeComponent();
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