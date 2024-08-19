using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Transports.GetTransportDetails;
using Warehouse.Application.Transports.GetTransports;
using Warehouse.Application.Transports.Models;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Warehouse;
using ErrorWindow = Warehouse.UI.Views.Management.Dialogs.Sectors.Components.ErrorWindow;

namespace Warehouse.UI.ViewModels.Warehouse;

public sealed class WarehouseViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly ISender _sender;

    public ObservableCollection<TransportModel> Transports { get; private init; } = [];
    public ObservableCollection<TransportModel> FilteredTransports { get; } = [];

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        private set => SetProperty(ref _isLoading, value);
    }

    private bool _isTransportsViewed;
    public bool IsTransportsViewed
    {
        get => _isTransportsViewed;
        set => SetProperty(ref _isTransportsViewed, value);
    }

    public ViewedTransport ViewedTransports
    {
        get => _viewedTransports;
        set => SetProperty(ref _viewedTransports, value);
    }

    private string _numberSearchQuery = string.Empty;
    public string NumberSearchQuery
    {
        get => _numberSearchQuery;
        set
        {
            SetProperty(ref _numberSearchQuery, value);
            ApplyFilters();
        }
    }

    private string _warehousemanSearchQuery = string.Empty;
    public string WarehousemanSearchQuery
    {
        get => _warehousemanSearchQuery;
        set
        {
            SetProperty(ref _warehousemanSearchQuery, value);
            ApplyFilters();
        }
    }

    private string _vehicleSearchQuery = string.Empty;
    public string VehicleSearchQuery
    {
        get => _vehicleSearchQuery;
        set
        {
            SetProperty(ref _vehicleSearchQuery, value);
            ApplyFilters();
        }
    }

    private string _clientSearchQuery = string.Empty;

    public string ClientSearchQuery
    {
        get => _clientSearchQuery;
        set
        {
            SetProperty(ref _clientSearchQuery, value);
            ApplyFilters();
        }
    }

    private bool _isImportsLocked;
    public bool IsImportsLocked
    {
        get => _isImportsLocked;
        set => SetProperty(ref _isImportsLocked, value);
    }

    private bool _isExportsLocked;
    private ViewedTransport _viewedTransports;

    public bool IsExportsLocked
    {
        get => _isExportsLocked;
        set => SetProperty(ref _isExportsLocked, value);
    }

    public AsyncRelayCommand<Guid> OpenTransportCommand { get; }
    public RelayCommand OpenAddTransportCommand { get; }
    public AsyncRelayCommand OpenImportsCommand { get; }
    public AsyncRelayCommand OpenExportsCommand { get; }

    public WarehouseViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();

        OpenImportsCommand = new AsyncRelayCommand(LoadImports());
        OpenExportsCommand = new AsyncRelayCommand(LoadExports());
        OpenTransportCommand = new AsyncRelayCommand<Guid>(NavigateToTransportDetails);
        OpenAddTransportCommand = new RelayCommand(NavigateToAddTransport);
    }

    private Func<Task> LoadExports() => async () => await FetchTransports(ViewedTransport.Exports);

    private Func<Task> LoadImports() => async () => await FetchTransports(ViewedTransport.Imports);

    private void NavigateToAddTransport() =>
        _mainWindow.ContentArea.Content = ViewedTransports switch
        {
            ViewedTransport.Imports => new AddImportView(_mainWindow),
            ViewedTransport.Exports => new AddExportView(_mainWindow),
            _ => _mainWindow.ContentArea.Content
        };

    private async Task NavigateToTransportDetails(Guid transportId)
    {
        IsLoading = true;

        var query = new GetTransportDetailsQuery(transportId);

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        var transport = result.Value;

        IsLoading = false;
        _mainWindow.ContentArea.Content = new TransportDetailsView(_mainWindow, transport);
    }

    private async Task FetchTransports(ViewedTransport type)
    {
        IsLoading = true;
        ViewedTransports = type;
        IsTransportsViewed = true;

        _ = type switch
        {
            ViewedTransport.Imports => (IsExportsLocked, IsImportsLocked) = (false, true),
            ViewedTransport.Exports => (IsExportsLocked, IsImportsLocked) = (true, false),
            _ => (IsExportsLocked, IsImportsLocked) = (false, false)
        };

        var query = new GetTransportsQuery(type.AsString());

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        var transports = result.Value;

        Transports.Clear();
        Transports.AddRange(transports);
        ApplyFilters();
        OnPropertyChanged(nameof(Transports));
        IsLoading = false;
    }

    private void ApplyFilters()
    {
        var filtered = Transports
            .Where(
                t => t.Client.Nip.Contains(ClientSearchQuery, StringComparison.OrdinalIgnoreCase) ||
                     t.Client.Name.Contains(ClientSearchQuery, StringComparison.OrdinalIgnoreCase))
            .Where(
                t => t.Warehouseman.IdentificationNumber.ToString()
                         .Contains(
                             WarehousemanSearchQuery,
                             StringComparison.OrdinalIgnoreCase) ||
                     t.Warehouseman.FirstName.Contains(
                         WarehousemanSearchQuery,
                         StringComparison.OrdinalIgnoreCase) ||
                     t.Warehouseman.LastName.Contains(
                         WarehousemanSearchQuery,
                         StringComparison.OrdinalIgnoreCase))
            .Where(
                t => t.Driver?.VehiclePlate.Contains(
                         VehicleSearchQuery,
                         StringComparison.OrdinalIgnoreCase) ??
                     true)
            .Where(
                t => t.Number.ToString().Contains(NumberSearchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();

        FilteredTransports.Clear();
        FilteredTransports.AddRange(filtered);
    }
}