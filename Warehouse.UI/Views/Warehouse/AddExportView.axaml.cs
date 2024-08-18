using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DocumentFormat.OpenXml.Vml;
using DynamicData;
using Warehouse.Application.Freights.Models;
using Warehouse.UI.ViewModels.Warehouse;

namespace Warehouse.UI.Views.Warehouse;

public partial class AddExportView : UserControl
{
    private readonly AddExportViewModel _dataContext;

    public AddExportView(MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = new AddExportViewModel(mainWindow);
        _dataContext = (AddExportViewModel)DataContext;

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        await _dataContext.InitAsync();
    }

    private void OnFreightSelect(object? sender, RoutedEventArgs e)
    {
        var checkbox = sender as CheckBox;
        var freight = checkbox?.DataContext as FreightModel;

        switch (checkbox?.IsChecked)
        {
            case true:
                _dataContext.SelectFreight(freight);
                break;
            case false:
                _dataContext.UnselectFreight(freight);
                break;
        }
    }
}