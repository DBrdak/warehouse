using System;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Clients.Models;
using Warehouse.Application.Drivers.Models;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.UI.Views;
using DynamicData;
using Microsoft.IdentityModel.Tokens;
using Warehouse.Application.Clients.GetAllClients;
using Warehouse.Application.Drivers.GetAllDrivers;
using Warehouse.Application.Freights.GetAllImports;
using Warehouse.Application.Freights.Models;
using Warehouse.Application.Freights.ReleaseFreight;
using Warehouse.Application.Transports.HandleTransport;
using Warehouse.Application.Warehousemen.GetWarehousemen;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;
using Warehouse.UI.Views.Management.Dialogs.Sectors.Components;
using Warehouse.UI.Views.Warehouse;

namespace Warehouse.UI.ViewModels.Warehouse;

internal class AddExportViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly ISender _sender;

    private bool _isLoading;

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public ObservableCollection<WarehousemanModel> Warehousemen { get; } = [];
    public ObservableCollection<ClientModel> Clients { get; } = [];
    public ObservableCollection<DriverModel> Drivers { get; } = [];
    public ObservableCollection<FreightModel> Freights { get; } = [];
    public ObservableCollection<FreightModel> SelectedFreights { get; } = [];

    private WarehousemanModel? _warehouseman;
    public WarehousemanModel? Warehouseman
    {
        get => _warehouseman;
        set
        {
            SetProperty(ref _warehouseman, value);
            IsTransportSubmitAllowed = Warehouseman is not null &&
                                       Client is not null &&
                                       Driver is not null &&
                                       !SelectedFreights.IsNullOrEmpty();
        }
    }

    private ClientModel? _client;

    public ClientModel? Client
    {
        get => _client;
        set
        {
            SetProperty(ref _client, value);
            IsTransportSubmitAllowed = Warehouseman is not null &&
                                       Client is not null &&
                                       Driver is not null &&
                                       !SelectedFreights.IsNullOrEmpty();
        }
    }

    private DriverModel? _driver;
    public DriverModel? Driver
    {
        get => _driver;
        set
        {
            SetProperty(ref _driver, value);
            IsTransportSubmitAllowed = Warehouseman is not null &&
                                       Client is not null &&
                                       Driver is not null &&
                                       !SelectedFreights.IsNullOrEmpty();
        }
    }

    private bool _isTransportSubmitAllowed;
    public bool IsTransportSubmitAllowed
    {
        get => _isTransportSubmitAllowed;
        set => SetProperty(ref _isTransportSubmitAllowed, value);
    }

    public AsyncRelayCommand SubmitAsyncCommand { get; }
    public RelayCommand CancelCommand { get; }

    public AddExportViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = mainWindow.ServiceProvider.GetRequiredService<ISender>();

        SubmitAsyncCommand = new AsyncRelayCommand(AddTransportAsync);
        CancelCommand = new RelayCommand(ExitToTransports);
    }

    public async Task InitAsync()
    {
        IsLoading = true;

        var (warehousemenQuery, clientsQuery, driversQuery, freightsQuery) =
            (new GetWarehousemenQuery(), new GetAllClientsQuery(), new GetAllDriversQuery(), new GetAllImportedFreightsQuery());

        var warehousemenGetResult = await _sender.Send(warehousemenQuery);
        var clientsGetResult = await _sender.Send(clientsQuery);
        var driversGetResult = await _sender.Send(driversQuery);
        var freightsGetResult = await _sender.Send(freightsQuery);

        if (Result.Aggregate(
                warehousemenGetResult,
                clientsGetResult,
                driversGetResult,
                freightsGetResult) is var result &&
            result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Warehousemen.Clear();
        Clients.Clear();
        Drivers.Clear();
        Freights.Clear();
        Warehousemen.AddRange(warehousemenGetResult.Value);
        Clients.AddRange(clientsGetResult.Value);
        Drivers.AddRange(driversGetResult.Value);
        Freights.AddRange(freightsGetResult.Value);
        IsLoading = false;
    }

    private async Task AddTransportAsync()
    {
        if (Warehouseman is null ||
            Driver is null ||
            Client is null ||
            SelectedFreights.IsNullOrEmpty())
        {
            await new ErrorWindow(Error.InvalidValue.Message).ShowDialog(_mainWindow);
            return;
        }

        IsLoading = true;

        var transportHandleCommand = new HandleTransportCommand(
            TransportType.Export.Value,
            Warehouseman.Id,
            Driver.Id,
            Client.Id,
            DateTime.UtcNow);

        var transportAddResult = await _sender.Send(transportHandleCommand);

        if (transportAddResult.IsFailure)
        {
            await new ErrorWindow(transportAddResult.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        var transport = transportAddResult.Value;

        var freightsReceiveCommand = new ReleaseFreightCommand(transport.Id, SelectedFreights);

        var freightsReceiveResult = await _sender.Send(freightsReceiveCommand);

        if (freightsReceiveResult.IsFailure)
        {
            await new ErrorWindow(freightsReceiveResult.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        IsLoading = false;
        ExitToTransports();
    }

    public void SelectFreight(FreightModel freight)
    {
        SelectedFreights.Add(freight);
        IsTransportSubmitAllowed = Warehouseman is not null &&
                                   Client is not null &&
                                   Driver is not null &&
                                   !SelectedFreights.IsNullOrEmpty();
    }

    public void UnselectFreight(FreightModel freight)
    {
        SelectedFreights.Remove(freight);
        IsTransportSubmitAllowed = Warehouseman is not null &&
                                   Client is not null &&
                                   Driver is not null &&
                                   !SelectedFreights.IsNullOrEmpty();
    }

    private void ExitToTransports() => _mainWindow.ContentArea.Content = new WarehouseView(_mainWindow);
}