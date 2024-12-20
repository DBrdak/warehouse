﻿using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DynamicData;
using Warehouse.Application.Sectors.GetSectors;
using Warehouse.Application.Sectors.Models;
using Warehouse.Application.Warehousemen.UpdateWarehouseman;
using Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen.Models;
using Warehouse.UI.Views;
using ErrorWindow = Warehouse.UI.Views.Management.Dialogs.Sectors.Components.ErrorWindow;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

internal class EditWarehousemanDialogModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly Window _window;
    private readonly WarehousemenViewModel _invoker;
    private readonly ISender _sender;

    private Guid _warehousemanId;
    public Guid WarehousemanId
    {
        get => _warehousemanId;
        set => SetProperty(ref _warehousemanId, value);
    }

    private WarehousemanCreateModel _warehouseman;

    public WarehousemanCreateModel Warehouseman
    {
        get => _warehouseman;
        set => SetProperty(ref _warehouseman, value);
    }

    private int _sectorNumber;
    public int SectorNumber
    {
        get => _sectorNumber;
        set
        {
            SetProperty(ref _sectorNumber, value);
            Warehouseman = Warehouseman with { SectorNumber = value };
        }
    }

    public ObservableCollection<SectorModel> Sectors { get; } = [];

    public IAsyncRelayCommand EditWarehousemanAsyncCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public EditWarehousemanDialogModel(MainWindow mainWindow, Window window, WarehousemenViewModel invoker)
    {
        _mainWindow = mainWindow;
        _window = window;
        _invoker = invoker;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
        Warehouseman = invoker.SelectedWarehouseman is not null and var warehouseman ?
            WarehousemanCreateModel.FromExisting(warehouseman) :
            WarehousemanCreateModel.Init();
        WarehousemanId = invoker.SelectedWarehouseman?.Id ?? Guid.NewGuid();

        EditWarehousemanAsyncCommand = new AsyncRelayCommand(EditWarehousemanAsync);
        CancelCommand = new RelayCommand(Close);
    }

    private async Task EditWarehousemanAsync()
    {
        var command = new UpdateWarehousemanCommand(
            WarehousemanId,
            Warehouseman.FirstName,
            Warehouseman.LastName,
            Warehouseman.Position,
            Warehouseman.SectorNumber);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        await _invoker.FetchWarehousemenAsync();
        _invoker.SelectedWarehouseman = null;
        _invoker.IsWarehousemanSelected = false;
        _window.Close();
    }

    public async Task FetchSectorsAsync()
    {
        var query = new GetSectorsQuery();

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        var sectors = result.Value;

        Sectors.Clear();
        Sectors.AddRange(sectors);
    }

    private void Close() => _window.Close();
}