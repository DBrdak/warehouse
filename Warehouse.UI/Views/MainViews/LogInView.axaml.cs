using System;
using Avalonia.Controls;
using Avalonia.Input;
using Warehouse.UI.ViewModels.LogIn;

namespace Warehouse.UI.Views.MainViews;

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

    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        var a = (LoginViewModel)DataContext!;

        a.LoginCommand.Execute(sender);
    }
}