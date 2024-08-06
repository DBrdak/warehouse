using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.ViewModels.Management.Dialogs;

namespace Warehouse.UI.Views.Management.Dialogs;

public partial class AddSectorDialog : Window
{
    private readonly AddSectorDialogModel _dataContext;

    public AddSectorDialog()
    {
        InitializeComponent();
    }

    public AddSectorDialog(MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = new AddSectorDialogModel(mainWindow, this);
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