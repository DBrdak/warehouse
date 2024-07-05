using Avalonia.Controls;
using System;
using Warehouse.UI.ViewModels.LogIn;

namespace Warehouse.UI.Views;

public partial class LogInView : UserControl
{
    public LogInView()
    {
        InitializeComponent();
    }

    public LogInView(IServiceProvider serviceProvider, MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = new LoginViewModel(serviceProvider, mainWindow);
    }
}