using CommunityToolkit.Mvvm.ComponentModel;

namespace Warehouse.UI.ViewModels;

public class ViewModelBase : ObservableObject
{
    private string? user = null;

    public string? User
    {
        get => user;
        protected set => SetProperty(ref user, value);
    }
}