using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ReactiveUI;

namespace Warehouse.UI.ViewModels.Management.Dialogs;

public sealed class RackCreateModel : ReactiveObject
{
    public int RackNumber { get; private set; }
    public ObservableCollection<ShelfCreateModel> Shelves { get; init; }
    private static int _lastRackNumber = 0;

    public RackCreateModel()
    {
        RackNumber = ++_lastRackNumber;
        Shelves = [];
    }

    public void AddShelf() => Shelves.Add(new(Shelves.Count + 1));

    public static void RackRemoved() => _lastRackNumber--;

    public void RemoveShelf() => Shelves.RemoveAt(Shelves.Count - 1);
}