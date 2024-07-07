using System;
using System.Collections.Specialized;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.Application.Drivers.Models;
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
        InvalidateDataGrid();
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        var lodge = DataContext as LodgeViewModel;
        await lodge.FetchDrivers();
    }

    private async void DataGrid_OnRowEditEnded(object? sender, DataGridRowEditEndedEventArgs e)
    {
        if (e.EditAction != DataGridEditAction.Commit)
        {
            return;
        }

        var driver = e.Row.DataContext as DriverModel;
        var lodge = DataContext as LodgeViewModel;

        if (driver == lodge.PlaceholderDriver &&
            driver.FirstName != string.Empty &&
            driver.LastName != string.Empty &&
            driver.VehiclePlate != string.Empty)
        {
            await lodge!.AddDriverCommand.ExecuteAsync(driver);
            lodge.ExitCreateMode();
            return;
        }

        if (driver == lodge.PlaceholderDriver)
        {
            lodge.Drivers.Remove(driver);
            lodge.ExitCreateMode();
            return;
        }

        var previousState = lodge.SelectedDriver;
        var isStateChanged = previousState != driver;

        if (!isStateChanged)
        {
            return;
        }

        await lodge!.EditDriverCommand.ExecuteAsync(driver);
        lodge.SelectedDriver = null;
    }

    private void DataGrid_OnBeginningEdit(object? sender, DataGridBeginningEditEventArgs e)
    {
        var driver = e.Row.DataContext as DriverModel;
        var lodge = DataContext as LodgeViewModel;

        lodge.SelectedDriver = driver.Copy();
    }

    private void RemoveButton_OnClick(object? sender, RoutedEventArgs e)
    {
    }

    private void InvalidateDataGrid()
    {
        var lodge = DataContext as LodgeViewModel;
        var dataGrid = this.FindControl<DataGrid>("DriversDataGrid");
        dataGrid.InvalidateArrange();
        dataGrid.InvalidateVisual();
        dataGrid.InvalidateMeasure();
    }
}