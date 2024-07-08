using Avalonia.Controls;
using Avalonia.Input;
using Warehouse.UI.Stores;
using Warehouse.UI.ViewModels.LogIn;

namespace Warehouse.UI.Views.MainViews;

public partial class LogInView : UserControl
{
    public LogInView()
    {
        InitializeComponent();
        UserStore.CurrentUser = null;
    }

    public LogInView(MainWindow mainWindow)
    {
        InitializeComponent();
        UserStore.CurrentUser = null;
        DataContext = new LoginViewModel(mainWindow);
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