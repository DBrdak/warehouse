using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
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

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        await _dataContext.FetchSectorsAsync();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void IdNumberChanged(object? sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        var input = textBox?.Text ?? "";
        var parsedInput = 0;

        while (input.Length != 0 && !int.TryParse(input, out parsedInput))
        {
            input = input[..^1];
        }
        
        _dataContext.NewWarehouseman = _dataContext.NewWarehouseman with { IdentificationNumber = parsedInput.ToString() };
    }
}