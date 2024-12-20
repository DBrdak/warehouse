using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.Application.Sectors.Models;
using Warehouse.UI.ViewModels.Management.Dialogs.Sectors;

namespace Warehouse.UI.Views.Management.Dialogs.Sectors;

public partial class UpdateSectorDialog : Window
{
    private readonly SectorModel _selectedSector;
    private readonly UpdateSectorDialogModel _dataContext;

    public UpdateSectorDialog()
    {
        InitializeComponent();
    }

    public UpdateSectorDialog(SectorModel selectedSector)
    {
        InitializeComponent();
        _selectedSector = selectedSector;
        DataContext = new UpdateSectorDialogModel();
        _dataContext = DataContext as UpdateSectorDialogModel ??
                       throw new InvalidCastException(
                           $"Cannot convert type {DataContext.GetType().Name} to {nameof(UpdateSectorDialogModel)}");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}