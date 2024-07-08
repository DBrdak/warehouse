using CommunityToolkit.Mvvm.Input;
using System;
using Warehouse.UI.Views;
using System.Collections.Generic;
using Warehouse.UI.Views.Management;
using CustomerServiceView = Warehouse.UI.Views.CustomerService.CustomerServiceView;
using LodgeView = Warehouse.UI.Views.Lodge.LodgeView;
using MainDashboardView = Warehouse.UI.Views.MainViews.MainDashboardView;
using WarehouseView = Warehouse.UI.Views.Warehouse.WarehouseView;

namespace Warehouse.UI.ViewModels.LogIn;

public partial class LoginViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private string _message;

    private readonly MainWindow _mainWindow;

    public LoginViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        LoginCommand = new RelayCommand(OnLogin, CanLogin);
        PropertyChanged += (_, _) =>
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

    private void NavigateToMainDashboard()
    {
        User = "admin";
        _mainWindow.ContentArea.Content = new MainDashboardView(_mainWindow);
    }

    private void NavigateToWarehouse()
    {
        User = "magazynier";
        _mainWindow.ContentArea.Content = new WarehouseView(_mainWindow);
    }

    private void NavigateToCustomerService()
    {
        User = "handlowiec";
        _mainWindow.ContentArea.Content = new CustomerServiceView(_mainWindow);
    }

    private void NavigateToManagement()
    {
        User = "manager";
        _mainWindow.ContentArea.Content = new ManagementView(_mainWindow);
    }

    private void NavigateToLodge()
    {
        User = "ochrona";
        _mainWindow.ContentArea.Content = new LodgeView(_mainWindow);
    }

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