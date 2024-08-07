using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Warehouse.UI.Views;

namespace Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

internal class EditWarehousemanDialogModel
{
    private readonly MainWindow _mainWindow;
    private readonly Window _window;
    private readonly WarehousemenViewModel _invoker;
    private readonly ISender _sender;

    public IAsyncRelayCommand EditSectorAsyncCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public EditWarehousemanDialogModel(MainWindow mainWindow, Window window, WarehousemenViewModel invoker)
    {
        _mainWindow = mainWindow;
        _window = window;
        _invoker = invoker;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();

        EditSectorAsyncCommand = new AsyncRelayCommand(EditWarehousemanAsync);
        CancelCommand = new RelayCommand(Close);
    }

    private async Task EditWarehousemanAsync()
    {

    }

    private void Close() => _window.Close();
}