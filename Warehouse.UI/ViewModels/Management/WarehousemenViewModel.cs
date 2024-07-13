using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.UI.Views;

namespace Warehouse.UI.ViewModels.Management;

public sealed class WarehousemenViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;

    public WarehousemenViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }
}