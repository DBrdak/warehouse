using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Warehouse.Application.Freights.Models;
using Warehouse.Application.Freights.ReceiveFreights;

namespace Warehouse.UI.Converters;

internal class QuantityUnitToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is FreightCreateModel freight ? freight.Quantity.ToString($"##.## {freight.Unit}") :
            value is FreightModel freight1 ? freight1.Quantity.ToString($"##.## {freight1.Unit}") : value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}