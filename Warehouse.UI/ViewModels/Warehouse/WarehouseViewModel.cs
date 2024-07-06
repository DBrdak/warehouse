using Warehouse.UI.Views;

namespace Warehouse.UI.ViewModels.Warehouse;

public sealed class WarehouseViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;

    public WarehouseViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }
}