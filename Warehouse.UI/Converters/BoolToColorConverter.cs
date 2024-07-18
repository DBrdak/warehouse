using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Avalonia.Media;

namespace Warehouse.UI.Converters;

internal class BoolToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            true => new SolidColorBrush(Color.Parse("#00C853")), // Light Green
            false => new SolidColorBrush(Color.Parse("#FF1744")), // Red
            _ => new SolidColorBrush(Color.Parse("#9E9E9E")) // Grey
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}