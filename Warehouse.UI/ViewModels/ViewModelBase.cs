using CommunityToolkit.Mvvm.ComponentModel;
using System.Runtime.CompilerServices;
using Warehouse.UI.Stores;

namespace Warehouse.UI.ViewModels;

public class ViewModelBase : ObservableObject
{
    public string? User
    {
        get => UserStore.CurrentUser;
        protected set => UserStore.CurrentUser = value;
    }
}