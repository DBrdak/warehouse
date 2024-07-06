using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Warehouse.UI.Views;
using Warehouse.UI.Views.Management;
using CustomerServiceView = Warehouse.UI.Views.CustomerService.CustomerServiceView;
using LodgeView = Warehouse.UI.Views.Lodge.LodgeView;
using WarehouseView = Warehouse.UI.Views.Warehouse.WarehouseView;

namespace Warehouse.UI.ViewModels.MainDashboard;

internal class MainDashboardViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;

    public ICommand NavigateToLodgeCommand { get; }
    public ICommand NavigateToWarehouseCommand { get; }
    public ICommand NavigateToCustomerServiceCommand { get; }
    public ICommand NavigateToManagementCommand { get; }

    public MainDashboardViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        NavigateToLodgeCommand = new RelayCommand(NavigateToLodge);
        NavigateToWarehouseCommand = new RelayCommand(NavigateToWarehouse);
        NavigateToCustomerServiceCommand = new RelayCommand(NavigateToCustomerService);
        NavigateToManagementCommand = new RelayCommand(NavigateToManagement);
    }

    public void NavigateToWarehouse() => _mainWindow.ContentArea.Content = new WarehouseView();

    public void NavigateToCustomerService() => _mainWindow.ContentArea.Content = new CustomerServiceView();

    public void NavigateToManagement() => _mainWindow.ContentArea.Content = new ManagementView();

    public void NavigateToLodge() => _mainWindow.ContentArea.Content = new LodgeView();
}