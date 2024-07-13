using CommunityToolkit.Mvvm.Input;
using Warehouse.UI.Views;

namespace Warehouse.UI.ViewModels.Management;

public sealed class ManagementViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;

    private object? _currentView;
    public object? CurrentView
    {
        get => _currentView;
        set
        {
            if (!SetProperty(ref _currentView, value)) return;
            IsWarehousemenLocked = value?.GetType() == typeof(WarehousemenViewModel);
            IsSectorsLocked = value?.GetType() == typeof(SectorsViewModel);
        }
    }

    private bool _isWarehousemenLocked;
    public bool IsWarehousemenLocked
    {
        get => _isWarehousemenLocked;
        private set => SetProperty(ref _isWarehousemenLocked, value);
    }

    private bool _isSectorsLocked;
    public bool IsSectorsLocked
    {
        get => _isSectorsLocked;
        private set => SetProperty(ref _isSectorsLocked, value);
    }

    public RelayCommand ShowWarehousemenCommand { get; }
    public RelayCommand ShowSectorsCommand { get; }

    public ManagementViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;

        ShowWarehousemenCommand = new RelayCommand(ShowWarehousemen);
        ShowSectorsCommand = new RelayCommand(ShowSectors);
    }

    private void ShowWarehousemen()
    {
        CurrentView = new WarehousemenViewModel(_mainWindow);
    }

    private void ShowSectors()
    {
        CurrentView = new SectorsViewModel(_mainWindow);
    }
}