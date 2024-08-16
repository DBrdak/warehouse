using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Warehouse.UI.Converters;

internal class RackNumberToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int rackNumber)
        {
            return $"Regał {rackNumber}";
        }

        return "Miejsca paletowe:";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}