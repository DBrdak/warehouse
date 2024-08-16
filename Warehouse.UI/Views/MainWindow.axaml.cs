using System;
using System.ComponentModel.Design;
using Avalonia.Controls;
using Warehouse.UI.Views.MainViews;

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

        var logInView = new LogInView(this);
        ContentArea.Content = logInView;
    }
}