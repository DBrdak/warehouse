using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Clients.Models;
using Warehouse.Application.Clients.AddClient;
using Warehouse.Application.Clients.GetAllClients;
using Warehouse.Application.Clients.RemoveClient;
using Warehouse.Application.Clients.UpdateClient;
using Warehouse.Application.Reports.Clients;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Components;

namespace Warehouse.UI.ViewModels.CustomerService;

public sealed class CustomerServiceViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;
    private readonly ISender _sender;

    public ObservableCollection<ClientModel> Clients { get; } = new();

    private ObservableCollection<ClientModel> _filteredClients = new();
    public ObservableCollection<ClientModel> FilteredClients
    {
        get => _filteredClients;
        set => SetProperty(ref _filteredClients, value);
    }

    private string _nipSearchQuery = string.Empty;
    public string NipSearchQuery
    {
        get => _nipSearchQuery;
        set
        {
            SetProperty(ref _nipSearchQuery, value);
            ApplyFilters();
        }
    }

    private string _nameSearchQuery = string.Empty;
    public string NameSearchQuery
    {
        get => _nameSearchQuery;
        set
        {
            SetProperty(ref _nameSearchQuery, value);
            ApplyFilters();
        }
    }

    private ClientModel _placeholderClient = new();
    public ClientModel PlaceholderClient
    {
        get => _placeholderClient;
        set => SetProperty(ref _placeholderClient, value);
    }

    private bool _isInCreateMode;
    public bool IsInCreateMode
    {
        get => _isInCreateMode;
        set => SetProperty(ref _isInCreateMode, value);
    }


    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private ClientModel? _selectedClient;

    public ClientModel? SelectedClient
    {
        get => _selectedClient;
        set => SetProperty(ref _selectedClient, value);
    }

    public IRelayCommand AddPlaceholderCommand { get; }
    public IAsyncRelayCommand<ClientModel> AddClientCommand { get; }
    public IAsyncRelayCommand<ClientModel> EditClientCommand { get; }
    public IAsyncRelayCommand<ClientModel> RemoveClientCommand { get; }
    public IAsyncRelayCommand<ClientModel> GenerateReportCommand { get; set; }

    public CustomerServiceViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _sender = mainWindow.ServiceProvider.GetRequiredService<ISender>();

        AddPlaceholderCommand = new RelayCommand(AddPlaceholder);
        AddClientCommand = new AsyncRelayCommand<ClientModel>(AddClient);
        EditClientCommand = new AsyncRelayCommand<ClientModel>(EditClient);
        RemoveClientCommand = new AsyncRelayCommand<ClientModel>(RemoveClient);
        GenerateReportCommand = new AsyncRelayCommand<ClientModel>(GenerateReport);
    }

    public async Task FetchClients()
    {
        IsLoading = true;
        var query = new GetAllClientsQuery();

        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        var drivers = result.Value;

        Clients.Clear();
        Clients.AddRange(drivers);
        IsLoading = false;
        ApplyFilters();
        OnPropertyChanged(nameof(Clients));
    }

    private async Task AddClient(ClientModel? client)
    {
        if (client is null ||
            string.IsNullOrWhiteSpace(client.Name) ||
            string.IsNullOrWhiteSpace(client.Nip))
        {
            return;
        }

        IsLoading = true;

        var command = new AddClientCommand(client.Name, client.Nip);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Clients.Add(result.Value);
        IsLoading = false;
        ApplyFilters();
    }

    private async Task EditClient(ClientModel? client)
    {
        if (client is null)
        {
            return;
        }

        IsLoading = true;
        var command = new UpdateClientCommand(client.Id, client.Name, client.Nip);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Clients.Replace(client, result.Value);
        IsLoading = false;
        ApplyFilters();
    }

    private async Task RemoveClient(ClientModel? client)
    {
        if (client is null)
        {
            return;
        }

        IsLoading = true;
        var command = new RemoveClientCommand(client.Id);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        Clients.Remove(client);
        IsLoading = false;
        ApplyFilters();
    }

    private async Task GenerateReport(ClientModel? client)
    {
        if (client is null)
        {
            return;
        }

        IsLoading = true;
        var command = new GenerateClientReportCommand(client.Id);

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            await new ErrorWindow(result.Error.Message).ShowDialog(_mainWindow);
            return;
        }

        IsLoading = false;
    }

    private void AddPlaceholder()
    {
        IsInCreateMode = true;
        PlaceholderClient = new();
        Clients.Add(PlaceholderClient);
        ApplyFilters();
    }

    public void ExitCreateMode()
    {
        Clients.Remove(PlaceholderClient);
        PlaceholderClient = new();
        IsInCreateMode = false;
        ApplyFilters();
    }

    public void ApplyFilters()
    {
        var filtered = Clients
            .Where(d => d.Nip.Contains(NipSearchQuery, StringComparison.OrdinalIgnoreCase))
            .Where(d => d.Name.Contains(NameSearchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();

        FilteredClients.Clear();
        FilteredClients.AddRange(filtered);
    }
}