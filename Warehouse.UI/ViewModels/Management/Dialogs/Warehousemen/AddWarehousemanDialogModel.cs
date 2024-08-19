using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Warehousemen.HireWarehouseman;
using Warehouse.UI.Views;
using Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen.Models;
using System.Collections.ObjectModel;
using Warehouse.Application.Sectors.Models;
using DynamicData;
using Warehouse.Application.Sectors.GetSectors;
using ErrorWindow = Warehouse.UI.Views.Management.Dialogs.Sectors.Components.ErrorWindow;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen;

internal class AddWarehousemanDialogModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly Window _window;
    private readonly WarehousemenViewModel _invoker;
    private readonly ISender _sender;

    private WarehousemanCreateModel _newWarehouseman;

    public WarehousemanCreateModel NewWarehouseman
    {
        get => _newWarehouseman;
        set => SetProperty(ref _newWarehouseman, value);
    }

    private int _sectorNumber;
    public int SectorNumber
    {
        get => _sectorNumber;
        set
        {
            SetProperty(ref _sectorNumber, value);
            NewWarehouseman = NewWarehouseman with { SectorNumber = value };
        }
    }

    public ObservableCollection<SectorModel> Sectors { get; } = [];

    public IAsyncRelayCommand AddWarehousemanAsyncCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public AddWarehousemanDialogModel(MainWindow mainWindow, Window window, WarehousemenViewModel invoker)
    {
        _mainWindow = mainWindow;
        _window = window;
        _invoker = invoker;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
        NewWarehouseman = WarehousemanCreateModel.Init();

        AddWarehousemanAsyncCommand = new AsyncRelayCommand(AddWarehousemanAsync);
        CancelCommand = new RelayCommand(Close);
    }

    private async Task AddWarehousemanAsync()
    {
        var command = new HireWarehousemanCommand(
            int.TryParse(NewWarehouseman.IdentificationNumber, out var idNumber) ? idNumber : -1,
            NewWarehouseman.FirstName,
            NewWarehouseman.LastName,
            NewWarehouseman.Position,
            NewWarehouseman.SectorNumber ?? -1);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        _ = _invoker.FetchWarehousemenAsync();
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