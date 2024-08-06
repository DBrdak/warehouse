using ReactiveUI;

namespace Warehouse.UI.ViewModels.Management.Dialogs;

public sealed class ShelfCreateModel : ReactiveObject
{
    public int ShelfNumber { get; private set; }
    private int _palletSpacesCount;
    public int PalletSpacesCount
    {
        get => _palletSpacesCount;
        set => this.RaiseAndSetIfChanged(ref _palletSpacesCount, value);
    }

    public ShelfCreateModel(int shelfNumber)
    {
        ShelfNumber = shelfNumber;
        PalletSpacesCount = 0;
    }
}