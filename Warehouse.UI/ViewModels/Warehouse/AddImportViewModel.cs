using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Warehouse.Application.Clients.GetAllClients;
using Warehouse.Application.Clients.Models;
using Warehouse.Application.Drivers.GetAllDrivers;
using Warehouse.Application.Drivers.Models;
using Warehouse.Application.Freights.ReceiveFreights;
using Warehouse.Application.Sectors.GetSectors;
using Warehouse.Application.Sectors.Models;
using Warehouse.Application.Transports.HandleTransport;
using Warehouse.Application.Transports.RemoveTransport;
using Warehouse.Application.Warehousemen.GetWarehousemen;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Management.Dialogs.Sectors.Components;
using Warehouse.UI.Views.Warehouse;

namespace Warehouse.UI.ViewModels.Warehouse;

internal class AddImportViewModel : ViewModelBase
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
    public ObservableCollection<SectorModel> Sectors { get; } = [];
    public ObservableCollection<int> Racks { get; } = [];
    public ObservableCollection<int> Shelfs { get; } = [];
    public ObservableCollection<int> PalletSpaceNumbers { get; } = [];
    public ObservableCollection<FreightCreateModel> FreightsInput { get; } = [];

    private WarehousemanModel? _warehouseman;
    public WarehousemanModel? Warehouseman
    {
        get => _warehouseman;
        set
        {
            SetProperty(ref _warehouseman, value);
            IsSectorInputEnabled = Client is not null && Driver is not null && value is not null;
        }
    }

    private ClientModel? _client;

    public ClientModel? Client
    {
        get => _client;
        set
        {
            SetProperty(ref _client, value);
            IsSectorInputEnabled = value is not null && Driver is not null && Warehouseman is not null;
        }
    }

    private DriverModel? _driver;

    public DriverModel? Driver
    {
        get => _driver;
        set
        {
            SetProperty(ref _driver, value);
            IsSectorInputEnabled = Client is not null && value is not null && Warehouseman is not null;
        }
    }

    private SectorModel? _sector;
    public SectorModel? Sector
    {
        get => _sector;
        set
        {
            SetProperty(ref _sector, value);
            IsSectorInputEnabled = value is null;
            IsRackInputEnabled = value is not null;
            Racks.Clear();
            Racks.AddRange(value?.PalletSpaces?.Select(ps => ps.Rack).Distinct() ?? []);
            IsFreightInputInitialized = true;
        }
    }

    private int? _rack;
    public int? Rack
    {
        get => _rack;
        set
        {
            SetProperty(ref _rack, value);
            IsRackInputEnabled = false;
            IsShelfInputEnabled = value is not null;
            Shelfs.Clear();
            Shelfs.AddRange(
                _sector?.PalletSpaces?.Where(ps => ps.Rack == value)
                    .Select(ps => ps.Shelf).Distinct() ?? []);
        }
    }

    private int? _shelf;
    public int? Shelf
    {
        get => _shelf;
        set
        {
            SetProperty(ref _shelf, value);
            IsShelfInputEnabled = false;
            IsPalletSpaceInputEnabled = value is not null;
            PalletSpaceNumbers.Clear();
            PalletSpaceNumbers.AddRange(
                _sector?.PalletSpaces?.Where(ps => ps.Rack == Rack && ps.Shelf == value)
                    .Select(ps => ps.Number) ?? []);
        }
    }

    private int? _palletSpaceNumber;

    public int? PalletSpaceNumber
    {
        get => _palletSpaceNumber;
        set
        {
            SetProperty(ref _palletSpaceNumber, value);
            IsPalletSpaceInputEnabled = false;
            IsFreightNameInputEnabled = value is not null;
        }
    }

    private string? _freightName;
    public string? FreightName
    {
        get => _freightName;
        set
        {
            SetProperty(ref _freightName, value);
            IsFreightNameInputEnabled = false;
            IsFreightTypeInputEnabled = value is not null;
        }
    }

    private string? _freightType;
    public string? FreightType
    {
        get => _freightType;
        set
        {
            SetProperty(ref _freightType, value);
            IsFreightTypeInputEnabled = false;
            IsFreightQuantityInputEnabled = value is not null;
        }
    }

    private decimal? _freightQuantity;
    public decimal? FreightQuantity
    {
        get => _freightQuantity;
        set
        {
            SetProperty(ref _freightQuantity, value);
            IsFreightQuantityInputEnabled = false;
            IsFreightUnitInputEnabled = value is not null;
        }
    }

    private string? _freightUnit;
    public string? FreightUnit
    {
        get => _freightUnit;
        set
        {
            SetProperty(ref _freightUnit, value);
            IsFreightSubmitAllowed = !string.IsNullOrWhiteSpace(value);
        }
    }

    private bool _isSectorInputEnabled;
    private bool _isRackInputEnabled;
    private bool _isShelfInputEnabled;
    private bool _isPalletSpaceInputEnabled;
    private bool _isFreightNameInputEnabled;
    private bool _isFreightTypeInputEnabled;
    private bool _isFreightQuantityInputEnabled;
    private bool _isFreightUnitInputEnabled;
    private bool _isFreightSubmitAllowed;
    private bool _isTransportSubmitAllowed;
    private bool _isFreightInputInitialized;

    public bool IsSectorInputEnabled
    {
        get => _isSectorInputEnabled;
        set
        {
            SetProperty(ref _isSectorInputEnabled, value);
            IsRackInputEnabled = false;
        }
    }

    public bool IsRackInputEnabled
    {
        get => _isRackInputEnabled;
        set
        {
            SetProperty(ref _isRackInputEnabled, value);
            IsShelfInputEnabled = false;
        }
    }

    public bool IsShelfInputEnabled
    {
        get => _isShelfInputEnabled;
        set
        {
            SetProperty(ref _isShelfInputEnabled, value);
            IsPalletSpaceInputEnabled = false;
        }
    }

    public bool IsPalletSpaceInputEnabled
    {
        get => _isPalletSpaceInputEnabled;
        set
        {
            SetProperty(ref _isPalletSpaceInputEnabled, value);
            IsFreightNameInputEnabled = false;
        }
    }

    public bool IsFreightNameInputEnabled
    {
        get => _isFreightNameInputEnabled;
        set
        {
            SetProperty(ref _isFreightNameInputEnabled, value);
            IsFreightTypeInputEnabled = false;
        }
    }

    public bool IsFreightTypeInputEnabled
    {
        get => _isFreightTypeInputEnabled;
        set
        {
            SetProperty(ref _isFreightTypeInputEnabled, value);
            IsFreightQuantityInputEnabled = false;
        }
    }

    public bool IsFreightQuantityInputEnabled
    {
        get => _isFreightQuantityInputEnabled;
        set
        {
            SetProperty(ref _isFreightQuantityInputEnabled, value);
            IsFreightUnitInputEnabled = false;
        }
    }

    public bool IsFreightUnitInputEnabled
    {
        get => _isFreightUnitInputEnabled;
        set => SetProperty(ref _isFreightUnitInputEnabled, value);
    }

    public bool IsFreightSubmitAllowed
    {
        get => _isFreightSubmitAllowed;
        set => SetProperty(ref _isFreightSubmitAllowed, value);
    }

    public bool IsFreightInputInitialized
    {
        get => _isFreightInputInitialized;
        set => SetProperty(ref _isFreightInputInitialized, value);
    }

    public bool IsTransportSubmitAllowed
    {
        get => _isTransportSubmitAllowed;
        set => SetProperty(ref _isTransportSubmitAllowed, value);
    }

    public AsyncRelayCommand SubmitAsyncCommand { get; }
    public RelayCommand SubmitFreightInputCommand { get; }
    public RelayCommand CancelFreightInpuCommand { get; }
    public RelayCommand CancelCommand { get; }

    public AddImportViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = mainWindow.ServiceProvider.GetRequiredService<ISender>();

        SubmitAsyncCommand = new AsyncRelayCommand(AddTransportAsync);
        SubmitFreightInputCommand = new RelayCommand(AddFreight);
        CancelFreightInpuCommand = new RelayCommand(ClearFreightInputs);
        CancelCommand = new RelayCommand(ExitToTransports);
    }

    public async Task InitAsync()
    {
        IsLoading = true;

        var (warehousemenQuery, clientsQuery, driversQuery, sectorsQuery) =
            (new GetWarehousemenQuery(), new GetAllClientsQuery(), new GetAllDriversQuery(),
                new GetSectorsQuery(GetSectorQueryType.IncludePalletSpaces));

        var warehousemenGetResult = await _sender.Send(warehousemenQuery);
        var clientsGetResult = await _sender.Send(clientsQuery);
        var driversGetResult = await _sender.Send(driversQuery);
        var sectorsGetResult = await _sender.Send(sectorsQuery);

        if (Result.Aggregate(
                warehousemenGetResult,
                clientsGetResult,
                driversGetResult,
                sectorsGetResult) is var result &&
            result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Warehousemen.Clear();
        Clients.Clear();
        Drivers.Clear();
        Sectors.Clear();
        Warehousemen.AddRange(warehousemenGetResult.Value);
        Clients.AddRange(clientsGetResult.Value);
        Drivers.AddRange(driversGetResult.Value);
        Sectors.AddRange(sectorsGetResult.Value);
        IsLoading = false;
    }

    private async Task AddTransportAsync()
    {
        if (Warehouseman is null ||
            Driver is null ||
            Client is null ||
            FreightsInput.IsNullOrEmpty())
        {
            await new ErrorWindow(Error.InvalidValue.Message).ShowDialog(_mainWindow);
            return;
        }

        IsLoading = true;

        var transportHandleCommand = new HandleTransportCommand(
            TransportType.Import.Value,
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

        var freightsReceiveCommand = new ReceiveFreightsCommand(transport.Id, FreightsInput);

        var freightsReceiveResult = await _sender.Send(freightsReceiveCommand);

        if (freightsReceiveResult.IsFailure)
        {
            await new ErrorWindow(freightsReceiveResult.Error.Message).ShowDialog(_mainWindow);
            _ = await _sender.Send(new RemoveTransportCommand(transport.Id));
            return;
        }

        IsLoading = false;
        ExitToTransports();
    }

    private void AddFreight()
    {
        if (!IsFreightInputValid)
        {
            ClearFreightInputs();
            return;
        }

        FreightsInput.Add(new (
            Sector!.Number,
            Rack!.Value,
            Shelf!.Value,
            PalletSpaceNumber!.Value,
            FreightName!,
            FreightType!,
            FreightQuantity!.Value,
            FreightUnit!));
        ClearFreightInputs();
        IsTransportSubmitAllowed = Warehouseman is not null && Client is not null && Driver is not null;
    }

    public bool IsFreightInputValid =>
        Sector is not null &&
        Rack is not null &&
        Shelf is not null &&
        PalletSpaceNumber is not null &&
        FreightName is not null &&
        FreightType is not null &&
        FreightQuantity is not null &&
        FreightUnit is not null;

    private void ClearFreightInputs()
    {
        IsSectorInputEnabled = true;
        Sector = null;
        Racks.Clear();
        Rack = null;
        Shelfs.Clear();
        Shelf = null;
        PalletSpaceNumbers.Clear();
        PalletSpaceNumber = null;
        FreightName = null;
        FreightType = null;
        FreightQuantity = null;
        FreightUnit = null;
        IsFreightSubmitAllowed = false;
        IsFreightInputInitialized = false;
    }

    private void ExitToTransports() => _mainWindow.ContentArea.Content = new WarehouseView(_mainWindow);
}