using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Sectors.Models;
using Warehouse.UI.Views;

namespace Warehouse.UI.ViewModels.Management;

public sealed class SectorsViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;

    public SectorsViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    public ObservableCollection<SectorModel> Sectors { get; } = new();
}