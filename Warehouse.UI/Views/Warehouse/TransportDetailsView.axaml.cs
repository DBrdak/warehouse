using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.Application.Transports.Models;
using Warehouse.UI.ViewModels.Warehouse;

namespace Warehouse.UI.Views.Warehouse;

public partial class TransportDetailsView : UserControl
{
    private readonly TransportDetailsViewModel _dataContext;

    public TransportDetailsView(MainWindow mainWindow, TransportModel transport)
    {
        InitializeComponent();
        DataContext = new TransportDetailsViewModel(mainWindow, transport);
        _dataContext = (TransportDetailsViewModel)DataContext;

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        await _dataContext.InitAsync();
    }
}