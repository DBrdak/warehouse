using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Transports;

public sealed record TransportType
{
    public string Value { get; init; }

    public static readonly TransportType Import = new("Import");
    public static readonly TransportType Export = new("Export");
    private static readonly TransportType[] _all = [Import, Export];

    public bool IsImport => this == Import;
    public bool IsExport => this == Export;

    private TransportType(string value)
    {
        Value = value;
    }

    internal static Result<TransportType> Create(string value) =>
        (Result<TransportType>?)_all.FirstOrDefault(
            t => string.Equals(t.Value, value, StringComparison.CurrentCultureIgnoreCase)) ??
        TransportErrors.InvalidTransportType;
}