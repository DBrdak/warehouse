using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.ViewModels.Management.Dialogs.Sectors;
using Warehouse.UI.ViewModels.Management.Dialogs.Sectors.Models;

namespace Warehouse.UI.Views.Management.Dialogs.Sectors;

public partial class AddSectorDialog : Window
{
    private readonly AddSectorDialogModel _dataContext;

    public AddSectorDialog()
    {
        InitializeComponent();
    }

    public AddSectorDialog(MainWindow mainWindow, SectorsViewModel invoker)
    {
        InitializeComponent();
        DataContext = new AddSectorDialogModel(mainWindow, this, invoker);
        _dataContext = DataContext as AddSectorDialogModel ??
                       throw new InvalidCastException(
                           $"Cannot convert type {DataContext.GetType().Name} to {nameof(AddSectorDialogModel)}");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnShelfAdd(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var rack = button.DataContext as RackCreateModel;

        if (rack is null)
        {
            return;
        }

        _dataContext.AddShelf(rack);
    }

    private void OnShelfRemove(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var rack = button.DataContext as RackCreateModel;

        if (rack is null)
        {
            return;
        }

        _dataContext.RemoveShelf(rack);
    }

    private void UpdateIsValid(object? sender, TextChangedEventArgs e)
    {
        _dataContext.UpdateIsValid();
    }
}