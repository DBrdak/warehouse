using Avalonia.Data.Converters;
using Material.Icons;
using System;
using System.Globalization;

namespace Warehouse.UI.Converters;

internal class BoolToIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            true => MaterialIconKind.CheckCircle,
            false => MaterialIconKind.Cancel,
            _ => MaterialIconKind.Help,
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}