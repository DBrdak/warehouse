using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Warehouse.UI.ViewModels.Warehouse;

namespace Warehouse.UI.Converters;

internal sealed class ViewedTransportToAddTransportTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            ViewedTransport.Imports => "Nowy import",
            ViewedTransport.Exports => "Nowy eksport",
            _ => string.Empty
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;
}