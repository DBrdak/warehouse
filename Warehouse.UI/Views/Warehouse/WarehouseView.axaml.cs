using Avalonia.Controls;
using Warehouse.UI.ViewModels.Warehouse;

namespace Warehouse.UI.Views.Warehouse;

public partial class WarehouseView : UserControl
{
    public WarehouseView()
    {
        InitializeComponent();
    }

    public WarehouseView(MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = new WarehouseViewModel(mainWindow);
    }
}