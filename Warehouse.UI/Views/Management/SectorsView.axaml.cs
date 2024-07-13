using Avalonia.Controls;
using Warehouse.UI.ViewModels.Management;

namespace Warehouse.UI.Views.Management;

public partial class SectorsView : UserControl
{
    private readonly MainWindow _mainWindow;

    public SectorsView()
    {
        InitializeComponent();
    }

    public SectorsView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new ManagementViewModel(mainWindow);
        //Loaded += OnLoaded
    }

    private void DataGrid_OnBeginningEdit(object? sender, DataGridBeginningEditEventArgs e)
    {
    }

    private void DataGrid_OnRowEditEnded(object? sender, DataGridRowEditEndedEventArgs e)
    {
    }
}