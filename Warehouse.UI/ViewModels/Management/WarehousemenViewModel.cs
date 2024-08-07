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
using Warehouse.UI.Views.Management.Dialogs.Warehousemen;

namespace Warehouse.UI.ViewModels.Management;

public sealed class WarehousemenViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly ISender _sender;

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private WarehousemanModel _selectedWarehouseman;
    public WarehousemanModel SelectedWarehouseman
    {
        get => _selectedWarehouseman;
        set => SetProperty(ref _selectedWarehouseman, value);
    }

    public ObservableCollection<WarehousemanModel> Warehousemen { get; }

    public IAsyncRelayCommand AddWorkhousemanAsyncCommand { get; }
    public IAsyncRelayCommand EditWorkhousemanAsyncCommand { get; }
    public IAsyncRelayCommand RemoveWorkhousemanAsyncCommand { get; }

    public WarehousemenViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = mainWindow.ServiceProvider.GetRequiredService<ISender>();
        Warehousemen = [];

        AddWorkhousemanAsyncCommand = new AsyncRelayCommand(ShowAddWarehousemanDialog);
        EditWorkhousemanAsyncCommand = new AsyncRelayCommand(ShowEditWarehousemanDialog);
        RemoveWorkhousemanAsyncCommand = new AsyncRelayCommand(ShowRemoveWarehousemanDialog);
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

        var sectors = result.Value;

        Warehousemen.Clear();
        Warehousemen.AddRange(sectors);
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
            SelectedWarehouseman ?? throw new NullReferenceException("Warehouseman must be selected for update"));
        await dialog.ShowDialog(_mainWindow);
    }

    private async Task ShowRemoveWarehousemanDialog()
    {
        var dialog = new RemoveWarehousemanDialog(_mainWindow, this);
        await dialog.ShowDialog(_mainWindow);
    }
}