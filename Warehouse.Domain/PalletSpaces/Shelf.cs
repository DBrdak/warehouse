using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

public sealed record Shelf
{
    public int Value { get; init; }

    private Shelf(int value) => Value = value;

    internal static Result<Shelf> Create(int value)
    {
        var isValid = value > 0;

        if (!isValid)
        {
            return PalletSpaceErrors.InvalidShelfNumber;
        }

        return new Shelf(value);
    }
}