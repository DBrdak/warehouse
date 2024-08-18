using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Warehouse.Application.Freights.ReceiveFreights;
using Warehouse.UI.ViewModels.Warehouse;

namespace Warehouse.UI.Converters;

internal class QuantityUnitToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is FreightCreateModel freight ? freight.Quantity.ToString($"##.## {freight.Unit}") : value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}