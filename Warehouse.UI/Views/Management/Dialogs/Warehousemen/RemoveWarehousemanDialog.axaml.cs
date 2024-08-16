using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

namespace Warehouse.UI.Views.Management.Dialogs.Warehousemen;

public partial class RemoveWarehousemanDialog : Window
{
    private readonly RemoveWarehousemanDialogModel _dataContext;

    public RemoveWarehousemanDialog()
    {
        InitializeComponent();
    }

    public RemoveWarehousemanDialog(MainWindow mainWindow, WarehousemenViewModel invoker)
    {
        InitializeComponent();
        DataContext = new RemoveWarehousemanDialogModel(mainWindow, this, invoker);
        _dataContext = DataContext as RemoveWarehousemanDialogModel ??
                       throw new InvalidCastException(
                           $"Cannot convert type {DataContext.GetType().Name} to {nameof(RemoveWarehousemanDialogModel)}");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}