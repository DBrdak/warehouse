using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Extensions;
using Warehouse.UI.Stores;
using Warehouse.UI.ViewModels.Lodge;
using Warehouse.UI.Views.MainViews;

namespace Warehouse.UI.Views.Lodge;

public partial class LodgeView : UserControl
{
    private readonly MainWindow _mainWindow;

    public LodgeView()
    {
        InitializeComponent();
    }

    public LodgeView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new LodgeViewModel(mainWindow);
        Loaded += OnLoaded;
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

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        TextBox[] searchBars =
        [
            this.FindControl<TextBox>("FirstNameSearchBar"),
            this.FindControl<TextBox>("LastNameSearchBar"),
            this.FindControl<TextBox>("VehiclePlateSearchBar")
        ];
        var isAnySearchApplied = searchBars.Any(x => !string.IsNullOrEmpty(x.Text));

        if (isAnySearchApplied)
        {
            return;
        }

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
        var button = sender as Button;
        var driver = button.DataContext as DriverModel;
        var lodge = DataContext as LodgeViewModel;

        lodge.RemoveDriverCommand.ExecuteAsync(driver);
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        var mainWindow = _mainWindow;

        mainWindow.ContentArea.Content = UserStore.CurrentUser == "admin" ?
                new MainDashboardView(_mainWindow) :
                new LogInView(_mainWindow);
    }
}