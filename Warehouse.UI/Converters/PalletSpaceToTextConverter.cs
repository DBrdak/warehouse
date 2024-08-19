using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Warehouse.Application.PalletSpaces.Models;

namespace Warehouse.UI.Converters;

internal sealed class PalletSpaceToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is PalletSpaceModel palletSpace ?
            $"{palletSpace.Sector.Number}/{palletSpace.Rack}/{palletSpace.Shelf}/{palletSpace.Number}" :
            value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}