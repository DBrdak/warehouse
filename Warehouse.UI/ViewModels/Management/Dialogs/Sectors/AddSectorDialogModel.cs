using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Warehouse.Application.Sectors.AddSector;
using Warehouse.UI.ViewModels.Management.Dialogs.Sectors.Models;
using Warehouse.UI.Views;
using ErrorWindow = Warehouse.UI.Views.Management.Dialogs.Sectors.Components.ErrorWindow;

namespace Warehouse.UI.ViewModels.Management.Dialogs.Sectors;

public sealed class AddSectorDialogModel : ReactiveObject
{
    private readonly MainWindow _mainWindow;
    private readonly Window _window;
    private readonly SectorsViewModel _invoker;
    private readonly ISender _sender;

    private int _sectorNumber;

    public int SectorNumber
    {
        get => _sectorNumber;
        set
        {
            this.RaiseAndSetIfChanged(ref _sectorNumber, value);
            UpdateIsValid();
        }
    }

    public ObservableCollection<RackCreateModel> SectorRacks { get; }

    private bool _isValid;
    public bool IsValid
    {
        get => _isValid;
        private set => this.RaiseAndSetIfChanged(ref _isValid, value);
    }

    public IAsyncRelayCommand AddSectorAsyncCommand { get; }
    public IRelayCommand CancelCommand { get; }
    public IRelayCommand AddRackCommand { get; }
    public IRelayCommand RemoveRackCommand { get; }

    public AddSectorDialogModel(MainWindow mainWindow, Window window, SectorsViewModel invoker)
    {
        _mainWindow = mainWindow;
        _window = window;
        _invoker = invoker;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
        SectorRacks = new ObservableCollection<RackCreateModel>();

        AddSectorAsyncCommand = new AsyncRelayCommand(AddSectorAsync);
        CancelCommand = new RelayCommand(Close);
        AddRackCommand = new RelayCommand(AddRack);
        RemoveRackCommand = new RelayCommand(RemoveRack);
    }

    public void UpdateIsValid()
    {
        IsValid = _sectorNumber > 0 &&
                  SectorRacks.Count > 0 &&
                  SectorRacks.Any(rack => rack.Shelves.Any(shelf => shelf.PalletSpacesCount > 0));
    }

    private async Task AddSectorAsync()
    {
        var racksAddModels = SectorRacks.Select(
            rack => new SectorRackAddModel(
                rack.RackNumber,
                rack.Shelves.Select(
                    shelf => new SectorRackShelfAddModel(shelf.ShelfNumber, shelf.PalletSpacesCount))));

        var command = new AddSectorCommand(SectorNumber, racksAddModels);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        await _invoker.FetchSectors();
        _window.Close();
    }

    private void AddRack()
    {
        var newRack = new RackCreateModel();
        SectorRacks.Add(newRack);
        UpdateIsValid();
    }

    private void RemoveRack()
    {
        if (!SectorRacks.Any())
        {
            return;
        }

        SectorRacks.RemoveAt(SectorRacks.Count - 1);
        RackCreateModel.RackRemoved();
        UpdateIsValid();
    }

    public void AddShelf(RackCreateModel rack) => rack.AddShelf();

    public void RemoveShelf(RackCreateModel rack)
    {
        rack.RemoveShelf();

        UpdateIsValid();
    }

    private void Close() => _window.Close();
}
