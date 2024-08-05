using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.ViewModels.Management.Dialogs;

namespace Warehouse.UI.Views.Management.Dialogs;

public partial class AddSectorDialog : Window
{
    private readonly AddSectorDialogModel _dataContext;

    public AddSectorDialog()
    {
        InitializeComponent();
    }

    public AddSectorDialog(MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = new AddSectorDialogModel(mainWindow, this);
        _dataContext = DataContext as AddSectorDialogModel ??
                       throw new InvalidCastException(
                           $"Cannot convert type {DataContext.GetType().Name} to {nameof(AddSectorDialogModel)}");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}