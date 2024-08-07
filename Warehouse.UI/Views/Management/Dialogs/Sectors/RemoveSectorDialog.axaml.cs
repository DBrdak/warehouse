using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.UI.ViewModels.Management;
using Warehouse.UI.ViewModels.Management.Dialogs.Sectors;

namespace Warehouse.UI.Views.Management.Dialogs.Sectors;

public partial class RemoveSectorDialog : Window
{
    private readonly RemoveSectorDialogModel _dataContext;

    public RemoveSectorDialog()
    {
        InitializeComponent();
    }

    public RemoveSectorDialog(MainWindow mainWindow, SectorsViewModel invoker)
    {
        InitializeComponent();
        DataContext = new RemoveSectorDialogModel(mainWindow, this, invoker);
        _dataContext = DataContext as RemoveSectorDialogModel ??
                       throw new InvalidCastException(
                           $"Cannot convert type {DataContext.GetType().Name} to {nameof(RemoveSectorDialogModel)}");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}