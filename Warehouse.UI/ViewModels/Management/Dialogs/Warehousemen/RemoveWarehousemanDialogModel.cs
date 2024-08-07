using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.UI.Views;

namespace Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

internal class RemoveWarehousemanDialogModel
{
    private readonly MainWindow _mainWindow;
    private readonly Window _window;
    private readonly WarehousemenViewModel _invoker;
    private readonly ISender _sender;

    public IAsyncRelayCommand RemoveWarehousemanAsyncCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public RemoveWarehousemanDialogModel(MainWindow mainWindow, Window window, WarehousemenViewModel invoker)
    {
        _mainWindow = mainWindow;
        _window = window;
        _invoker = invoker;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();

        RemoveWarehousemanAsyncCommand = new AsyncRelayCommand(RemoveWarehousemanAsync);
        CancelCommand = new RelayCommand(Close);
    }

    private async Task RemoveWarehousemanAsync()
    {

    }

    private void Close() => _window.Close();
}