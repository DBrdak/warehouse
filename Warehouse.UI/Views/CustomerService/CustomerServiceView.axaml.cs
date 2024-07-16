using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Clients.Models;
using Warehouse.Domain.Shared.Extensions;
using Warehouse.UI.Stores;
using Warehouse.UI.ViewModels.CustomerService;
using Warehouse.UI.Views.MainViews;

namespace Warehouse.UI.Views.CustomerService;

public partial class CustomerServiceView : UserControl
{
    private readonly MainWindow _mainWindow;

    public CustomerServiceView()
    {
        InitializeComponent();
        _mainWindow = new MainWindow();
    }

    public CustomerServiceView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new CustomerServiceViewModel(mainWindow);
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        var customerService = DataContext as CustomerServiceViewModel;
        await customerService!.FetchClients();
    }

    private async void DataGrid_OnRowEditEnded(object? sender, DataGridRowEditEndedEventArgs e)
    {
        if (e.EditAction != DataGridEditAction.Commit)
            return;

        var client = e.Row.DataContext as ClientModel;
        var customerService = DataContext as CustomerServiceViewModel;

        if (customerService == null || client == null)
        {
            return;
        }

        if (IsPlaceholderClient(client, customerService))
        {
            await HandleAddClientAsync(client, customerService);
            return;
        }

        await HandleEditClientAsync(client, customerService);
    }

    private static bool IsPlaceholderClient(
        ClientModel client,
        CustomerServiceViewModel customerService) =>
        client == customerService.PlaceholderClient;

    private async Task HandleAddClientAsync(ClientModel client, CustomerServiceViewModel customerService)
    {
        switch (IsClientEmpty(client))
        {
            case true:
                customerService.Clients.Remove(client);
                break;
            case false:
                await customerService.AddClientCommand.ExecuteAsync(client);
                break;
        }

        customerService.ExitCreateMode();
    }

    private static bool IsClientEmpty(ClientModel client)
    {
        return string.IsNullOrEmpty(client.Nip) ||
               string.IsNullOrEmpty(client.Name);
    }

    private async Task HandleEditClientAsync(ClientModel client, CustomerServiceViewModel customerService)
    {
        var dataGrid = this.FindControl<DataGrid>("ClientsDataGrid");
        switch (IsClientEmpty(client), HasClientStateChanged(client, customerService.SelectedClient))
        {
            case (false, true):
                await customerService.EditClientCommand.ExecuteAsync(client);
                break;
            case (true, _):
                customerService.Clients.Replace(client, customerService.SelectedClient);
                customerService.ApplyFilters();
                break;
        }

        customerService.SelectedClient = null;
    }

    private static bool HasClientStateChanged(ClientModel currentClient, ClientModel? previousClient)
    {
        return !Equals(previousClient, currentClient);
    }

    private void DataGrid_OnBeginningEdit(object? sender, DataGridBeginningEditEventArgs e)
    {
        var client = e.Row.DataContext as ClientModel;
        var customerService = DataContext as CustomerServiceViewModel;

        customerService.SelectedClient = client.Copy();
    }

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        TextBox[] searchBars =
        [
            this.FindControl<TextBox>("NipSearchBar"),
            this.FindControl<TextBox>("NameSearchBar"),
        ];
        var isAnySearchApplied = searchBars.Any(x => !string.IsNullOrEmpty(x.Text));

        if (isAnySearchApplied)
        {
            return;
        }

        var viewModel = (CustomerServiceViewModel)DataContext;
        viewModel.AddPlaceholderCommand.Execute(null);
        BeginEditOnNewItem();
    }

    private void BeginEditOnNewItem()
    {
        var dataGrid = this.FindControl<DataGrid>("ClientsDataGrid");
        var viewModel = (CustomerServiceViewModel)DataContext;
        var newItem = dataGrid.ItemsSource.Find<ClientModel>(viewModel.PlaceholderClient);

        if (newItem == null)
        {
            return;
        }

        dataGrid.ScrollIntoView(newItem, new DataGridTextColumn { DisplayIndex = 0 });
        dataGrid.SelectedItem = newItem;
        dataGrid.CurrentColumn = dataGrid.Columns[0];
        dataGrid.BeginEdit();
    }

    private void RemoveButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var client = button.DataContext as ClientModel;
        var customerService = DataContext as CustomerServiceViewModel;

        customerService.RemoveClientCommand.ExecuteAsync(client);
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        var mainWindow = _mainWindow;

        mainWindow.ContentArea.Content = UserStore.CurrentUser == "admin" ?
                new MainDashboardView(_mainWindow) :
                new LogInView(_mainWindow);
    }

    private void ReportButton_Click(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var driver = button.DataContext as ClientModel;
        var customerService = DataContext as CustomerServiceViewModel;

        customerService.GenerateReportCommand.ExecuteAsync(driver);
    }
}