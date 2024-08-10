using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;

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