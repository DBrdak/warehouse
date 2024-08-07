using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.ViewModels.Management.Dialogs.Sectors;
using Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

namespace Warehouse.UI.Views.Management.Dialogs.Warehousemen;

public partial class AddWarehousemanDialog : Window
{
    private readonly AddWarehousemanDialogModel _dataContext;

    public AddWarehousemanDialog()
    {
        InitializeComponent();
    }

    public AddWarehousemanDialog(MainWindow mainWindow, WarehousemenViewModel invoker)
    {
        InitializeComponent();
        DataContext = new AddWarehousemanDialogModel(mainWindow, this, invoker);
        _dataContext = DataContext as AddWarehousemanDialogModel ??
                       throw new InvalidCastException(
                           $"Cannot convert type {DataContext.GetType().Name} to {nameof(AddWarehousemanDialogModel)}");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}