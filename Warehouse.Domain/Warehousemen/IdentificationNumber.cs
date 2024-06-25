using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Warehousemen;

public sealed record IdentificationNumber
{
    public int Value { get; init; }

    private IdentificationNumber(int value) => Value = value;

    internal static Result<IdentificationNumber> Create(int value)
    {
        var isValid = value > 0;

        if (!isValid)
        {
            return WarehousemanErrors.InvalidWarehousemanIdentificationNumber;
        }

        return new IdentificationNumber(value);
    }
}