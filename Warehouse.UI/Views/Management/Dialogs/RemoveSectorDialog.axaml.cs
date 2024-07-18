using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using Warehouse.Application.Sectors.Models;
using Warehouse.UI.ViewModels.Management.Dialogs;

namespace Warehouse.UI.Views.Management.Dialogs;

public partial class RemoveSectorDialog : Window
{
    private readonly SectorModel _selectedSector;
    private readonly RemoveSectorDialogModel _dataContext;

    public RemoveSectorDialog()
    {
        InitializeComponent();
    }

    public RemoveSectorDialog(SectorModel selectedSector)
    {
        InitializeComponent();
        _selectedSector = selectedSector;
        DataContext = new RemoveSectorDialogModel();
        _dataContext = DataContext as RemoveSectorDialogModel ??
                       throw new InvalidCastException(
                           $"Cannot convert type {DataContext.GetType().Name} to {nameof(RemoveSectorDialogModel)}");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}