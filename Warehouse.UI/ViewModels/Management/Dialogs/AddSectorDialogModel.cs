using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Warehouse.Application.Drivers.Models;
using Warehouse.Application.Sectors.AddSector;
using Warehouse.Application.Sectors.Models;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Components;
using Unit = System.Reactive.Unit;

namespace Warehouse.UI.ViewModels.Management.Dialogs;

public sealed class AddSectorDialogModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly Window _window;
    private readonly ISender _sender;

    private int _sectorNumber = 0;
    public int SectorNumber
    {
        get => _sectorNumber;
        set => SetProperty(ref _sectorNumber, value);
    }

    public ObservableCollection<RackCreateModel> SectorRacks { get; init; }

    public IAsyncRelayCommand AddSectorAsyncCommand { get; }

    public AddSectorDialogModel(MainWindow mainWindow, Window window)
    {
        _mainWindow = mainWindow;
        _window = window;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
        SectorRacks = [];

        AddSectorAsyncCommand = new AsyncRelayCommand(AddSectorAsync);
    }

    private async Task AddSectorAsync()
    {
        var racksAddModels = SectorRacks.Select(
            rack => new SectorRackAddModel(
                rack.RackNumber,
                rack.Shelfs.Select(
                    shelf => new SectorRackShelfAddModel(shelf.ShelfNumber, shelf.PalletSpacesCount))));

        var command = new AddSectorCommand(SectorNumber, racksAddModels);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        _window.Close();

        return;
    }

    public void AddRack() => SectorRacks.Add(new ());

    public void RemoveRack()
    {
        SectorRacks.RemoveAt(SectorRacks.Count - 1);
        RackCreateModel.RackRemoved();
    }

    public void AddShelf(int rackNumber) => 
        SectorRacks.FirstOrDefault(r => r.RackNumber == rackNumber)?.AddShelf();

    public void SetPalletSpaceCountForShelf(int rackNumber, int shelfNumber, int palletSpaceCount) =>
        SectorRacks
            .FirstOrDefault(r => r.RackNumber == rackNumber)?.Shelfs
            .FirstOrDefault(s => s.ShelfNumber == shelfNumber)?
            .SetPalletSpaceCount(palletSpaceCount);
}

public sealed record RackCreateModel
{
    public int RackNumber { get; private set; }
    public ObservableCollection<ShelfCreateModel> Shelfs { get; init; }
    private static int _lastRackNumber = 0;

    public RackCreateModel()
    {
        RackNumber = ++_lastRackNumber;
        Shelfs = [];
    }

    public void AddShelf() => Shelfs.Add(new(Shelfs.Count + 1));

    public static void RackRemoved() => _lastRackNumber--;
}

public sealed record ShelfCreateModel
{
    public int ShelfNumber { get; private set; }
    public int PalletSpacesCount { get; private set; }

    public ShelfCreateModel(int shelfNumber)
    {
        ShelfNumber = shelfNumber;
        PalletSpacesCount = 0;
    }

    public void SetPalletSpaceCount(int count) => 
        PalletSpacesCount = count > 0 ? count : PalletSpacesCount;
}