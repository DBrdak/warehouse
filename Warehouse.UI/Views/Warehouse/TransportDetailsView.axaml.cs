using Avalonia.Controls;
using Warehouse.Application.Transports.Models;

namespace Warehouse.UI.Views.Warehouse;

public partial class TransportDetailsView : UserControl
{
    public TransportDetailsView(MainWindow mainWindow, TransportModel transport)
    {
        InitializeComponent();
    }
}