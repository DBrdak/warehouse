using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Warehouse.Application.Drivers.Models;

namespace Warehouse.UI.Converters;

internal class DriverToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is DriverModel driver ?
            $"{driver.FirstName} {driver.LastName} {driver.VehiclePlate}" :
            value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}