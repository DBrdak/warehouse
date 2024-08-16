using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Warehouse.UI.ViewModels.Components;

internal class ErrorWindowViewModel : ViewModelBase
{
    private readonly Window _window;

    public ErrorWindowViewModel(string errorMessage, Window window)
    {
        ErrorMessage = errorMessage;
        _window = window;
        CloseCommand = new RelayCommand(CloseWindow);
    }

    public string ErrorMessage { get; }

    public ICommand CloseCommand { get; }

    private void CloseWindow()
    {
        _window.Close();
    }
}