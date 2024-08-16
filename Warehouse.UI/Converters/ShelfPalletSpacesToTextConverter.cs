using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Warehouse.UI.Converters;

internal class ShelfPalletSpacesToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int shelfNumber)
        {
            return $"[Półka {shelfNumber}] Miejsca paletowe:";
        }

        return "Miejsca paletowe:";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}