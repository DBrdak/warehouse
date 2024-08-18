using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

public sealed record Rack
{
    public int Value { get; init; }

    private Rack(int value) => Value = value;

    public static Result<Rack> Create(int value)
    {
        var isValid = value > 0;

        if (!isValid)
        {
            return PalletSpaceErrors.InvalidRackNumber;
        }

        return new Rack(value);
    }
}