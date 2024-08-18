using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

public sealed record PalletSpaceNumber
{
    public int Value { get; init; }

    private PalletSpaceNumber(int value) => Value = value;

    public static Result<PalletSpaceNumber> Create(int value)
    {
        var isValid = value > 0;

        if (!isValid)
        {
            return PalletSpaceErrors.InvalidNumber;
        }

        return new PalletSpaceNumber(value);
    }
}