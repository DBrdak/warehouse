using System;

namespace Warehouse.UI.ViewModels.Warehouse;

public enum ViewedTransport
{
    None,
    Imports,
    Exports
}

public static class ViewedTransportExtensions
{
    public static string AsString(this ViewedTransport viewedTransport) =>
        viewedTransport switch
        {
            ViewedTransport.None => string.Empty,
            ViewedTransport.Imports => "Import",
            ViewedTransport.Exports => "Export",
            _ => throw new ArgumentOutOfRangeException(nameof(viewedTransport), viewedTransport, null)
        };
}