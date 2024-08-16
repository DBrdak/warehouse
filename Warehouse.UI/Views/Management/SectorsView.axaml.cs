using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Warehouse.Application.Sectors.Models;
using Warehouse.UI.ViewModels.Management;

namespace Warehouse.UI.Views.Management;

public partial class SectorsView : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly SectorsViewModel _sectorsViewModel;

    public SectorsView()
    {
        InitializeComponent();
    }

    public SectorsView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new SectorsViewModel(_mainWindow);
        _sectorsViewModel = DataContext as SectorsViewModel ??
                            throw new InvalidCastException(
                                $"Cannot convert type {DataContext.GetType().Name} to {nameof(SectorsViewModel)}");
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        await _sectorsViewModel.FetchSectors();
    }

    private void OnSectorSelected(object? sender, SelectionChangedEventArgs e)
    {
        if(sender is ListBox { SelectedItem: SectorModel selectedSector })
        {
            _sectorsViewModel.SelectedSector = selectedSector;
        }
    }
}