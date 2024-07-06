using Avalonia.Controls;
using Warehouse.UI.ViewModels.Components;

namespace Warehouse.UI.Views.Components;

public partial class ErrorWindow : Window
{
    public ErrorWindow()
    {
        InitializeComponent();
    }

    public ErrorWindow(string errorMessage)
    {
        InitializeComponent();
        DataContext = new ErrorWindowViewModel(errorMessage, this);
    }
}