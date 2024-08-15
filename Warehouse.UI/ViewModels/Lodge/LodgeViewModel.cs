using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DynamicData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Drivers.AddDriver;
using Warehouse.Application.Drivers.GetAllDrivers;
using Warehouse.Application.Drivers.Models;
using Warehouse.Application.Drivers.RemoveDriver;
using Warehouse.Application.Drivers.UpdateDriver;
using Warehouse.UI.Views;
using System.Linq;
using Warehouse.Application.Reports.Drivers;
using ErrorWindow = Warehouse.UI.Views.Management.Dialogs.Sectors.Components.ErrorWindow;

namespace Warehouse.UI.ViewModels.Lodge;

public sealed class LodgeViewModel : ViewModelBase
{
    private readonly ISender _sender;
    private readonly MainWindow _mainWindow;

    public ObservableCollection<DriverModel> Drivers { get; } = new();

    private ObservableCollection<DriverModel> _filteredDrivers = new();
    public ObservableCollection<DriverModel> FilteredDrivers
    {
        get => _filteredDrivers;
        set => SetProperty(ref _filteredDrivers, value);
    }

    private string _firstNameSearchQuery = string.Empty;
    public string FirstNameSearchQuery
    {
        get => _firstNameSearchQuery;
        set
        {
            SetProperty(ref _firstNameSearchQuery, value);
            ApplyFilters();
        }
    }

    private string _lastNameSearchQuery = string.Empty;
    public string LastNameSearchQuery
    {
        get => _lastNameSearchQuery;
        set
        {
            SetProperty(ref _lastNameSearchQuery, value);
            ApplyFilters();
        }
    }

    private string _vehiclePlateSearchQuery = string.Empty;
    public string VehiclePlateSearchQuery
    {
        get => _vehiclePlateSearchQuery;
        set
        {
            SetProperty(ref _vehiclePlateSearchQuery, value);
            ApplyFilters();
        }
    }

    private DriverModel _placeholderDriver = new();
    public DriverModel PlaceholderDriver
    {
        get => _placeholderDriver;
        set => SetProperty(ref _placeholderDriver, value);
    }

    private bool _isInCreateMode;
    public bool IsInCreateMode
    {
        get => _isInCreateMode;
        set => SetProperty(ref _isInCreateMode, value);
    }


    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private DriverModel? _selectedDriver;

    public DriverModel? SelectedDriver
    {
        get => _selectedDriver;
        set => SetProperty(ref _selectedDriver, value);
    }

    public IRelayCommand AddPlaceholderCommand { get; }
    public IAsyncRelayCommand<DriverModel> AddDriverCommand { get; }
    public IAsyncRelayCommand<DriverModel> EditDriverCommand { get; }
    public IAsyncRelayCommand<DriverModel> RemoveDriverCommand { get; }
    public IAsyncRelayCommand<DriverModel> GenerateReportCommand { get; set; }

    public LodgeViewModel(MainWindow mainWindow)
    {
        _sender = mainWindow.ServiceProvider.GetRequiredService<ISender>();
        _mainWindow = mainWindow;

        AddPlaceholderCommand = new RelayCommand(AddPlaceholder);
        AddDriverCommand = new AsyncRelayCommand<DriverModel>(AddDriver);
        EditDriverCommand = new AsyncRelayCommand<DriverModel>(EditDriver);
        RemoveDriverCommand = new AsyncRelayCommand<DriverModel>(RemoveDriver);
        GenerateReportCommand = new AsyncRelayCommand<DriverModel>(GenerateReport);
    }

    public async Task FetchDrivers()
    {
        IsLoading = true;
        var query = new GetAllDriversQuery();

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        var drivers = result.Value;

        Drivers.Clear();
        Drivers.AddRange(drivers);
        IsLoading = false;
        ApplyFilters();
        OnPropertyChanged(nameof(Drivers));
    }

    private async Task AddDriver(DriverModel? driver)
    {
        if (driver is null ||
            string.IsNullOrWhiteSpace(driver.FirstName) ||
            string.IsNullOrWhiteSpace(driver.LastName) ||
            string.IsNullOrWhiteSpace(driver.VehiclePlate))
        {
            return;
        }

        IsLoading = true;

        var command = new AddDriverCommand(driver.FirstName, driver.LastName, driver.VehiclePlate);
        
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Drivers.Add(result.Value);
        IsLoading = false;
        ApplyFilters();
    }

    private async Task EditDriver(DriverModel? driver)
    {
        if (driver is null)
        {
            return;
        }

        IsLoading = true;
        var command = new UpdateDriverCommand(driver.Id, driver.FirstName, driver.LastName, driver.VehiclePlate);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Drivers.Replace(driver, result.Value);
        IsLoading = false;
        ApplyFilters();
    }

    private async Task RemoveDriver(DriverModel? driver)
    {
        if (driver is null)
        {
            return;
        }

        IsLoading = true;
        var command = new RemoveDriverCommand(driver.Id);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Drivers.Remove(driver);
        IsLoading = false;
        ApplyFilters();
    }

    private async Task GenerateReport(DriverModel? driver)
    {
        if (driver is null)
        {
            return;
        }

        IsLoading = true;
        var command = new GenerateDriverReportCommand(driver.Id);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        IsLoading = false;
    }

    private void AddPlaceholder()
    {
        IsInCreateMode = true;
        PlaceholderDriver = new();
        Drivers.Add(PlaceholderDriver);
        ApplyFilters();
    }

    public void ExitCreateMode()
    {
        Drivers.Remove(PlaceholderDriver);
        PlaceholderDriver = new();
        IsInCreateMode = false;
        ApplyFilters();
    }

    public void ApplyFilters()
    {
        var filtered = Drivers
            .Where(d => d.FirstName.Contains(FirstNameSearchQuery, StringComparison.OrdinalIgnoreCase))
            .Where(d => d.LastName.Contains(LastNameSearchQuery, StringComparison.OrdinalIgnoreCase))
            .Where(d => d.VehiclePlate.Contains(VehiclePlateSearchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();

        FilteredDrivers.Clear();
        FilteredDrivers.AddRange(filtered);
    }
}