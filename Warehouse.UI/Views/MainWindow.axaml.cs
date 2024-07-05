using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Warehouse.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        var logInView = new LogInView(serviceProvider, this);
        ContentArea.Content = logInView;
    }
}