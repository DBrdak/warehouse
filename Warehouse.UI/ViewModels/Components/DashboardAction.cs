using CommunityToolkit.Mvvm.Input;

namespace Warehouse.UI.ViewModels.Components;

public class DashboardAction
{
    public string Name { get; set; }
    public RelayCommand Command { get; set; }
}