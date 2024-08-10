using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Warehouse.Application.Warehousemen.FireWarehouseman;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Application.Warehousemen.UpdateWarehouseman;
using Warehouse.Domain.Warehousemen;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Components;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

internal class EditWarehousemanDialogModel : ViewModelBase
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

    public IAsyncRelayCommand EditSectorAsyncCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public EditWarehousemanDialogModel(MainWindow mainWindow, Window window, WarehousemenViewModel invoker)
    {
        _mainWindow = mainWindow;
        _window = window;
        _invoker = invoker;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
        Warehouseman = invoker.SelectedWarehouseman;

        EditSectorAsyncCommand = new AsyncRelayCommand(EditWarehousemanAsync);
        CancelCommand = new RelayCommand(Close);
    }

    private async Task EditWarehousemanAsync()
    {
        var command = new UpdateWarehousemanCommand(
            Warehouseman.Id,
            Warehouseman.FirstName,
            Warehouseman.LastName,
            Warehouseman.Position,
            Warehouseman.Sector?.Number);

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