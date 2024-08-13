using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Warehousemen.GetWarehousemen;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.UI.Views.Management.Components;
using Warehouse.UI.Views.Management.Dialogs.Warehousemen;

namespace Warehouse.UI.ViewModels.Management;

public sealed class WarehousemenViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly ISender _sender;

    private bool _isLoading = true;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private WarehousemanModel? _selectedWarehouseman;
    public WarehousemanModel? SelectedWarehouseman
    {
        get => _selectedWarehouseman;
        set => SetProperty(ref _selectedWarehouseman, value);
    }

    private bool _isWarehousemanSelected;
    public bool IsWarehousemanSelected
    {
        get => _isWarehousemanSelected;
        set => SetProperty(ref _isWarehousemanSelected, value);
    }

    public ObservableCollection<WarehousemanModel> Warehousemen { get; }

    public IAsyncRelayCommand AddWarehousemanAsyncCommand { get; }
    public IAsyncRelayCommand EditWarehousemanAsyncCommand { get; }
    public IAsyncRelayCommand RemoveWarehousemanAsyncCommand { get; }

    public WarehousemenViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = mainWindow.ServiceProvider.GetRequiredService<ISender>();
        Warehousemen = [];

        AddWarehousemanAsyncCommand = new AsyncRelayCommand(ShowAddWarehousemanDialog);
        EditWarehousemanAsyncCommand = new AsyncRelayCommand(ShowEditWarehousemanDialog);
        RemoveWarehousemanAsyncCommand = new AsyncRelayCommand(ShowRemoveWarehousemanDialog);
    }

    public async Task FetchWarehousemenAsync()
    {
        IsLoading = true;

        var query = new GetWarehousemenQuery();

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        var warehousemen = result.Value;

        Warehousemen.Clear();
        Warehousemen.AddRange(warehousemen);
        IsLoading = false;
    }

    private async Task ShowAddWarehousemanDialog()
    {
        var dialog = new AddWarehousemanDialog(_mainWindow, this);
        await dialog.ShowDialog(_mainWindow);
    }

    private async Task ShowEditWarehousemanDialog()
    {
        var dialog = new EditWarehousemanDialog(
            _mainWindow,
            this);
        await dialog.ShowDialog(_mainWindow);
    }

    private async Task ShowRemoveWarehousemanDialog()
    {
        var dialog = new RemoveWarehousemanDialog(_mainWindow, this);
        await dialog.ShowDialog(_mainWindow);
    }
}