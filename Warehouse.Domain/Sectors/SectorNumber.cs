using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Sectors;

public sealed record SectorNumber
{
    public int Value { get; init; }

    private SectorNumber(int value) => Value = value;

    public static Result<SectorNumber> Create(int value)
    {
        var isValid = value > 0;

        if (!isValid)
        {
            return SectorErrors.InvalidSectorNumber;
        }

        return new SectorNumber(value);
    }
}