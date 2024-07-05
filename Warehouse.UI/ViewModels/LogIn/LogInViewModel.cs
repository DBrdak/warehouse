using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.UI.Views;
using Warehouse.UI.ViewModels.MainDashboard;
using System.Collections.Generic;
using Warehouse.UI.ViewModels.CustomerService;
using Warehouse.UI.ViewModels.Lodge;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.ViewModels.Warehouse;

namespace Warehouse.UI.ViewModels.LogIn;

public partial class LoginViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private string _message;

    private readonly IServiceProvider _serviceProvider;
    private readonly MainWindow _mainWindow;

    public LoginViewModel(IServiceProvider serviceProvider, MainWindow mainWindow)
    {
        _serviceProvider = serviceProvider;
        _mainWindow = mainWindow;
        LoginCommand = new RelayCommand(OnLogin, CanLogin);
        PropertyChanged += (sender, args) =>
        {
            if (CanLogin())
            {
                LoginCommand.NotifyCanExecuteChanged();
            }
        };
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public IRelayCommand LoginCommand { get; }

    private void OnLogin()
    {
        var isValidUser = LogInDictionary.TryGetValue((Username.ToLower(), Password), out var navigator);

        if (!isValidUser || navigator is null)
        {
            Message = "Nieprawidłowe dane uwierzytelniające";
            return;
        }

        navigator.Invoke();
    }

    private bool CanLogin() => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

    private void NavigateToMainDashboard() =>
        _mainWindow.ContentArea.Content = new MainDashboardView()
        {
            DataContext = new MainDashboardViewModel()
        };

    private void NavigateToWarehouse() =>
        _mainWindow.ContentArea.Content = new WarehouseView()
        {
            DataContext = new WarehouseViewModel()
        };

    private void NavigateToCustomerService() =>
        _mainWindow.ContentArea.Content = new CustomerServiceView()
        {
            DataContext = new CustomerServiceViewModel()
        };

    private void NavigateToManagement() =>
        _mainWindow.ContentArea.Content = new ManagementView()
        {
            DataContext = new ManagementViewModel()
        };

    private void NavigateToLodge() =>
        _mainWindow.ContentArea.Content = new LodgeView()
        {
            DataContext = new LodgeViewModel()
        };

    private Dictionary<(string username, string password), Action> LogInDictionary =>
        new()
        {
            { ("admin", "admin"), NavigateToMainDashboard },
            { ("magazynier", "magazynier"), NavigateToWarehouse },
            { ("ochrona", "ochrona"), NavigateToLodge },
            { ("handlowiec", "handlowiec"), NavigateToCustomerService },
            { ("manager", "manager"), NavigateToManagement },
        };
}