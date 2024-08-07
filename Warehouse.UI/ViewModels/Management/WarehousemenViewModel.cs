using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Sectors.GetSectors;
using Warehouse.Application.Warehousemen.GetWarehousemen;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Warehousemen;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Components;

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

    public ObservableCollection<WarehousemanModel> Warehousemen { get; }

    public IAsyncRelayCommand AddWorkhousemanAsyncCommand { get; }
    public IAsyncRelayCommand EditWorkhousemanAsyncCommand { get; }
    public IAsyncRelayCommand RemoveWorkhousemanAsyncCommand { get; }

    public WarehousemenViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = mainWindow.ServiceProvider.GetRequiredService<ISender>();
        Warehousemen = [];

        AddWorkhousemanAsyncCommand = new AsyncRelayCommand(AddWorkhousemanAsync);
        EditWorkhousemanAsyncCommand = new AsyncRelayCommand(EditWorkhousemanAsync);
        RemoveWorkhousemanAsyncCommand = new AsyncRelayCommand(RemoveWorkhousemanAsync);
    }

    private async Task FetchWarehousemenAsync()
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

    private async Task AddWorkhousemanAsync()
    {
    }

    private async Task RemoveWorkhousemanAsync()
    {
    }

    private async Task EditWorkhousemanAsync()
    {
    }
}