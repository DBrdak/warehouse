using Avalonia.Controls;
using Warehouse.UI.ViewModels.Management;

namespace Warehouse.UI.Views.Management;

public partial class WarehousemenView : UserControl
{
    private readonly MainWindow _mainWindow;

    public WarehousemenView()
    {
        InitializeComponent();
    }

    public WarehousemenView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new ManagementViewModel(mainWindow);
        //Loaded += OnLoaded
    }
}