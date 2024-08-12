using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.UI.ViewModels.Management;

namespace Warehouse.UI.Views.Management.Components;

public partial class WarehousemanCard : UserControl
{
    public event EventHandler<RoutedEventArgs>? Click = default;

    public WarehousemanCard()
    {
        InitializeComponent();
    }

    private void OnClick(object? sender, RoutedEventArgs e)
    {
        Click?.Invoke(sender, e);
    }
}