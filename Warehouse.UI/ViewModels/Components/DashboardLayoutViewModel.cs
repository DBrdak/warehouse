using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Warehouse.UI.ViewModels.Components;

public class DashboardLayoutViewModel : ObservableObject
{
    public ObservableCollection<DashboardAction> Actions { get; }

    private object _content;
    public object Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }

    public DashboardLayoutViewModel()
    {
        Actions = new ObservableCollection<DashboardAction>();
    }
}