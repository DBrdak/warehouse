using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Transports;

public sealed record TransportNumber
{
    public int Value { get; init; }

    private TransportNumber(int value)
    {
        Value = value;
    }

    internal static Result<TransportNumber> Create(int value)
    {
        var isValid = value > 0;

        if (!isValid)
        {
            return TransportErrors.InvalidTransportNumber;
        }

        return new TransportNumber(value);
    }
}