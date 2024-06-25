using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Transports;

public sealed record TransportType
{
    public string Value { get; init; }

    internal static readonly TransportType Import = new("Import");
    internal static readonly TransportType Export = new("Export");
    private static readonly TransportType[] _all = [Import, Export];

    private TransportType(string value)
    {
        Value = value;
    }

    internal static Result<TransportType> Create(string value) =>
        (Result<TransportType>?)_all.FirstOrDefault(
            t => string.Equals(t.Value, value, StringComparison.CurrentCultureIgnoreCase)) ??
        TransportErrors.InvalidTransportType;
}