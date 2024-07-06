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
using Warehouse.UI.Views.Components;
using Warehouse.Domain.Drivers;
using Warehouse.UI.ViewModels.Lodge.Requests;

namespace Warehouse.UI.ViewModels.Lodge;

public sealed class LodgeViewModel : ViewModelBase
{
    private readonly ISender _sender;
    private readonly MainWindow _mainWindow;

    public ObservableCollection<DriverModel> Drivers { get; } = new();

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
        set
        {
            SetProperty(ref _selectedDriver, value);
            OnPropertyChanged(nameof(IsDriverSelected));
        }
    }

    public bool IsDriverSelected => _selectedDriver is not null;

    public IAsyncRelayCommand<AddDriverRequest> AddDriverCommand { get; }
    public IAsyncRelayCommand<DriverModel> EditDriverCommand { get; }
    public IAsyncRelayCommand<DriverModel> RemoveDriverCommand { get; }

    public LodgeViewModel(MainWindow mainWindow)
    {
        _sender = mainWindow.ServiceProvider.GetRequiredService<ISender>();
        _mainWindow = mainWindow;

        AddDriverCommand = new AsyncRelayCommand<AddDriverRequest>(AddDriver);
        EditDriverCommand = new AsyncRelayCommand<DriverModel>(EditDriver);
        RemoveDriverCommand = new AsyncRelayCommand<DriverModel>(RemoveDriver);
    }

    public async Task FetchDrivers()
    {
        IsLoading = true;
        var query = new GetAllDriversQuery();

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
        }

        var drivers = result.Value;

        Drivers.Clear();
        drivers.ForEach(Drivers.Add);
        IsLoading = false;
    }

    private async Task AddDriver(AddDriverRequest? request)
    {
        if (request is null)
        {
            return;
        }

        IsLoading = true;

        var command = new AddDriverCommand(request.FirstName, request.LastName, request.VehiclePlate);
        
        var result = await _sender.Send(command);
        
        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
        }
        
        Drivers.Add(result.Value);
        IsLoading = false;
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
        }

        IsLoading = false;
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
        }

        Drivers.Remove(driver);
        IsLoading = false;
    }
}