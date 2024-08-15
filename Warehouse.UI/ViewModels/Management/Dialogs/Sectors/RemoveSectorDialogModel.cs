using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Sectors.Models;
using Warehouse.Application.Sectors.RemoveSector;
using Warehouse.UI.Views;
using ErrorWindow = Warehouse.UI.Views.Management.Dialogs.Sectors.Components.ErrorWindow;

namespace Warehouse.UI.ViewModels.Management.Dialogs.Sectors;

public sealed class RemoveSectorDialogModel : ViewModelBase
{
    private readonly SectorModel _sector;
    private readonly MainWindow _mainWindow;
    private readonly Window _window;
    private readonly SectorsViewModel _invoker;
    private readonly ISender _sender;

    public IAsyncRelayCommand RemoveSectorAsyncCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public RemoveSectorDialogModel(MainWindow mainWindow, Window window, SectorsViewModel invoker)
    {
        _mainWindow = mainWindow;
        _window = window;
        _invoker = invoker;
        _sender = _mainWindow.ServiceProvider.GetRequiredService<ISender>();
        _sector = _invoker.SelectedSector ?? throw new ArgumentNullException(nameof(_sector), "Nie podano sektora");
        
        RemoveSectorAsyncCommand = new AsyncRelayCommand(RemoveSectorAsync);
        CancelCommand = new RelayCommand(Close);
    }

    private async Task RemoveSectorAsync( )
    {
        var command = new RemoveSectorCommand(_sector.Id);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        await _invoker.FetchSectors();
        _invoker.SelectedSector = null;
        _window.Close();
    }

    private void Close() => _window.Close();
}