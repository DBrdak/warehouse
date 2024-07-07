using System;
using System.Collections.Specialized;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Extensions;
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
            return;

        var driver = e.Row.DataContext as DriverModel;
        var lodge = DataContext as LodgeViewModel;

        if (lodge == null || driver == null)
        {
            return;
        }

        if (IsPlaceholderDriver(driver, lodge))
        {
            await HandleAddDriverAsync(driver, lodge);
            return;
        }

        await HandleEditDriverAsync(driver, lodge);
    }

    private static bool IsPlaceholderDriver(
        DriverModel driver,
        LodgeViewModel lodge) =>
        driver == lodge.PlaceholderDriver;

    private async Task HandleAddDriverAsync(DriverModel driver, LodgeViewModel lodge)
    {
        switch (IsDriverEmpty(driver))
        {
            case true:
                lodge.Drivers.Remove(driver);
                break;
            case false:
                await lodge.AddDriverCommand.ExecuteAsync(driver);
                break;
        }

        lodge.ExitCreateMode();
    }

    private static bool IsDriverEmpty(DriverModel driver)
    {
        return string.IsNullOrEmpty(driver.FirstName) ||
               string.IsNullOrEmpty(driver.LastName) ||
               string.IsNullOrEmpty(driver.VehiclePlate);
    }

    private async Task HandleEditDriverAsync(DriverModel driver, LodgeViewModel lodge)
    {
        var dataGrid = this.FindControl<DataGrid>("DriversDataGrid");
        switch (IsDriverEmpty(driver), HasDriverStateChanged(driver, lodge.SelectedDriver))
        {
            case (false, true):
                await lodge.EditDriverCommand.ExecuteAsync(driver);
                break;
            case (true, _):
                lodge.Drivers.Replace(driver, lodge.SelectedDriver);
                lodge.ApplyFilters();
                break;
        }

        lodge.SelectedDriver = null;
    }

    private static bool HasDriverStateChanged(DriverModel currentDriver, DriverModel? previousDriver)
    {
        return !Equals(previousDriver, currentDriver);
    }

    private void DataGrid_OnBeginningEdit(object? sender, DataGridBeginningEditEventArgs e)
    {
        var driver = e.Row.DataContext as DriverModel;
        var lodge = DataContext as LodgeViewModel;

        lodge.SelectedDriver = driver.Copy();
    }

    private void Button_OnClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var viewModel = (LodgeViewModel)DataContext;
        viewModel.AddPlaceholderCommand.Execute(null);
        BeginEditOnNewItem();
    }

    private void BeginEditOnNewItem()
    {
        var dataGrid = this.FindControl<DataGrid>("DriversDataGrid");
        var viewModel = (LodgeViewModel)DataContext;
        var newItem = dataGrid.ItemsSource.Find<DriverModel>(viewModel.PlaceholderDriver);

        if (newItem == null)
        {
            return;
        }
        
        dataGrid.ScrollIntoView(newItem, new DataGridTextColumn{DisplayIndex = 0});
        dataGrid.SelectedItem = newItem;
        dataGrid.CurrentColumn = dataGrid.Columns[0];
        dataGrid.BeginEdit();
    }

    private void RemoveButton_OnClick(object? sender, RoutedEventArgs e)
    {
    }

    private void InvalidateDataGrid()
    {
        var dataGrid = this.FindControl<DataGrid>("DriversDataGrid");
        dataGrid.InvalidateArrange();
        dataGrid.InvalidateVisual();
        dataGrid.InvalidateMeasure();
        dataGrid.ScrollIntoView(null, new DataGridTextColumn());
    }
}