﻿using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.UI.Views;
using Warehouse.Application.Sectors.AddSector;
using Warehouse.Application.Warehousemen.FireWarehouseman;
using Warehouse.Domain.Sectors;
using Warehouse.UI.Views.Components;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Warehousemen;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

internal class RemoveWarehousemanDialogModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly Window _window;
    private readonly WarehousemenViewModel _invoker;
    private readonly ISender _sender;

    private WarehousemanModel? _warehouseman;
    public WarehousemanModel? Warehouseman
    {
        get => _warehouseman;
        set => SetProperty(ref _warehouseman, value);
    }

    public IAsyncRelayCommand RemoveWarehousemanAsyncCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public RemoveWarehousemanDialogModel(MainWindow mainWindow, Window window, WarehousemenViewModel invoker)
    {
        _mainWindow = mainWindow;
        _window = window;
        _invoker = invoker;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
        Warehouseman = invoker.SelectedWarehouseman;

        RemoveWarehousemanAsyncCommand = new AsyncRelayCommand(RemoveWarehousemanAsync);
        CancelCommand = new RelayCommand(Close);
    }

    private async Task RemoveWarehousemanAsync()
    {
        var command = new FireWarehousemanCommand(Warehouseman.Id);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        _ = _invoker.FetchWarehousemenAsync();
        _window.Close();
    }

    private void Close() => _window.Close();
}