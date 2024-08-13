using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Application.Sectors.GetSectors;
using Warehouse.Application.Sectors.Models;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Components;
using Warehouse.UI.Views.Management.Dialogs.Sectors;

namespace Warehouse.UI.ViewModels.Management;

public sealed class SectorsViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly ISender _sender;

    public ObservableCollection<SectorModel> Sectors { get; } = new();
    public ObservableCollection<PalletSpaceModel> SelectedSectorPalletSpaces { get; } = new();


    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        private set => SetProperty(ref _isLoading, value);
    }

    private SectorModel? _selectedSector;
    public SectorModel? SelectedSector
    {
        get => _selectedSector;
        set
        {
            SetProperty(ref _selectedSector, value);
            IsSectorSelected = value is not null;
            CanSectorBeRemoved = (value?.PalletSpaces is not null && value.PalletSpaces.All(ps => ps.IsAvailable)) ||
                                 value?.PalletSpaces is null;
            SelectedSectorPalletSpaces.Clear();
            SelectedSectorPalletSpaces.AddRange(value?.PalletSpaces ?? []);
        }
    }

    private bool _isSectorSelected;
    public bool IsSectorSelected
    {
        get => _isSectorSelected;
        private set => SetProperty(ref _isSectorSelected, value);
    }

    private bool _canSectorBeRemoved;
    public bool CanSectorBeRemoved
    {
        get => _canSectorBeRemoved;
        private set => SetProperty(ref _canSectorBeRemoved, value);
    }

    public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddSectorCommand { get; }
    public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> UpdateSectorCommand { get; }
    public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> RemoveSectorCommand { get; }

    public SectorsViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();

        AddSectorCommand = ReactiveCommand.Create(ShowAddSectorDialog);
        UpdateSectorCommand = ReactiveCommand.Create(ShowUpdateSectorDialog);
        RemoveSectorCommand = ReactiveCommand.Create(ShowRemoveSectorDialog);
    }

    public async Task FetchSectors()
    {
        IsLoading = true;

        var query = new GetSectorsQuery(GetSectorQueryType.Detailed);

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        var sectors = result.Value;

        Sectors.Clear();
        Sectors.AddRange(sectors);
        IsLoading = false;
    }

    private async void ShowAddSectorDialog()
    {
        var dialog = new AddSectorDialog(_mainWindow, this);
        await dialog.ShowDialog(_mainWindow);
    }

    private async void ShowUpdateSectorDialog()
    {
        var dialog = new UpdateSectorDialog(
            SelectedSector ?? throw new NullReferenceException("Sector must be selected for update"));
        await dialog.ShowDialog(_mainWindow);
    }

    private async void ShowRemoveSectorDialog()
    {
        var dialog = new RemoveSectorDialog(_mainWindow, this);
        await dialog.ShowDialog(_mainWindow);
    }
}