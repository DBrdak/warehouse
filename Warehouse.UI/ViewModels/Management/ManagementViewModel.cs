using Warehouse.UI.Views;

namespace Warehouse.UI.ViewModels.Management;

public sealed class ManagementViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;

    public ManagementViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }
}