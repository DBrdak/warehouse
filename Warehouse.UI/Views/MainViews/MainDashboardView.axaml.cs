using Avalonia.Controls;
using Warehouse.UI.ViewModels.MainDashboard;

namespace Warehouse.UI.Views.MainViews;

public partial class MainDashboardView : UserControl
{
    public MainDashboardView()
    {
        InitializeComponent();
    }

    public MainDashboardView(MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = new MainDashboardViewModel(mainWindow);
    }
}