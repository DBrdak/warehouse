using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Sectors.GetSectors;
using Warehouse.Application.Sectors.Models;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Components;

namespace Warehouse.UI.ViewModels.Management;

public sealed class SectorsViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly ISender _sender;

    public ObservableCollection<SectorModel> Sectors { get; } = new();

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
        set => SetProperty(ref _selectedSector, value);
    }

    public SectorsViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
    }

    public async Task FetchSectors()
    {
        IsLoading = true;

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
        IsLoading = false;
    }
}