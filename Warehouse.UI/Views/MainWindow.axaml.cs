using System;
using System.ComponentModel.Design;
using Avalonia.Controls;

namespace Warehouse.UI.Views;

public partial class MainWindow : Window
{
    public readonly IServiceProvider ServiceProvider;

    public MainWindow()
    {
        ServiceProvider = new ServiceContainer();
        InitializeComponent();
    }

    public MainWindow(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        InitializeComponent();

        var logInView = new MainViews.LogInView(ServiceProvider, this);
        ContentArea.Content = logInView;
    }
}