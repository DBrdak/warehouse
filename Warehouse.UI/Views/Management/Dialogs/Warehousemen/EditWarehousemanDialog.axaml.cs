using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Warehouse.UI.ViewModels.Management;
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

        _dataContext.Warehouseman = _dataContext.Warehouseman with { IdentificationNumber = parsedInput.ToString() };
    }

    private void SectorNumberChanged(object? sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        var selectionBoxItem = comboBox?.SelectionBoxItem as TextBlock;
        var selectedValue = selectionBoxItem?.Text;

        _dataContext.SectorNumber = int.Parse(selectedValue);
    }
}