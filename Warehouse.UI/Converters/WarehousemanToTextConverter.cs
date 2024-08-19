using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Warehouse.Application.Warehousemen.Models;

namespace Warehouse.UI.Converters;

internal class WarehousemanToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is WarehousemanModel warehouseman ?
            $"{warehouseman.LastName} {warehouseman.IdentificationNumber}" :
            value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}