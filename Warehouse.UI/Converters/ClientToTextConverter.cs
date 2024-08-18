using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Warehouse.Application.Clients.Models;

namespace Warehouse.UI.Converters;

internal class ClientToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is ClientModel client ?
            $"{client.Name} {client.Nip}" :
            value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}