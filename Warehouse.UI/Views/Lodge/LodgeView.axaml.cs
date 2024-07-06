using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Drivers;
using Warehouse.UI.ViewModels.Lodge;

namespace Warehouse.UI.Views.Lodge;

public partial class LodgeView : UserControl
{
    public LodgeView()
    {
        InitializeComponent();
    }

    public LodgeView(MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = new LodgeViewModel(mainWindow);
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var lodge = DataContext as LodgeViewModel;
        await lodge!.FetchDrivers();
    }

    private async void DataGrid_OnCellEditEnded(object? sender, DataGridCellEditEndedEventArgs e)
    {
        if (e.EditAction != DataGridEditAction.Commit)
        {
            return;
        }

        var driver = e.Row.DataContext as DriverModel;
        var lodge = DataContext as LodgeViewModel;

        await lodge!.EditDriverCommand.ExecuteAsync(driver);
    }

    private void InputElement_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        var dataGrid = sender as DataGrid;

        if (!dataGrid.IsFocused)
        {
            return;
        }

        dataGrid.CommitEdit();
    }

    private void InputElement_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        var dataGrid = sender as DataGrid;

        if (dataGrid.IsFocused)
        {
            return;
        }

        dataGrid.BeginEdit();
    }
}