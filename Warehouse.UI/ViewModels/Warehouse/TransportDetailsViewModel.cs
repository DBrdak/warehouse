using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Clients.GetAllClients;
using Warehouse.Application.Clients.Models;
using Warehouse.Application.Drivers.GetAllDrivers;
using Warehouse.Application.Drivers.Models;
using Warehouse.Application.Freights.Models;
using Warehouse.Application.Sectors.GetSectors;
using Warehouse.Application.Transports.GetTransportDetails;
using Warehouse.Application.Transports.Models;
using Warehouse.Application.Warehousemen.GetWarehousemen;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;
using Warehouse.Domain.Warehousemen;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Management.Dialogs.Sectors.Components;
using Warehouse.UI.Views.Warehouse;

namespace Warehouse.UI.ViewModels.Warehouse;

internal class TransportDetailsViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly ISender _sender;
    private readonly Guid _transportId;

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }
    
    private bool _isExport;
    public bool IsExport
    {
        get => _isExport;
        set => SetProperty(ref _isExport, value);
    }

    private TransportModel _transport;
    public TransportModel Transport
    {
        get => _transport;
        set => SetProperty(ref _transport, value);
    }

    public RelayCommand ExitCommand { get; }

    public TransportDetailsViewModel(MainWindow mainWindow, TransportModel transport)
    {
        _mainWindow = mainWindow;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
        _transportId = transport.Id;

        ExitCommand = new RelayCommand(ExitToTransports);
    }


    public async Task InitAsync()
    {
        IsLoading = true;

        var transportQuery = new GetTransportDetailsQuery(_transportId);

        var transportGetResult = await _sender.Send(transportQuery);

        if (transportGetResult.IsFailure)
        {
            await new ErrorWindow(transportGetResult.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Transport = transportGetResult.Value;
        IsExport = Transport.Type == TransportType.Export.Value;
        IsLoading = false;
    }

    private void ExitToTransports() => _mainWindow.ContentArea.Content = new WarehouseView(_mainWindow);
}