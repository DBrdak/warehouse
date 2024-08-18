using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Warehouse.UI.Converters;

internal class DateTimeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is DateTime date ? date.ToString("dd/MM/yyyy hh:mm") : value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}