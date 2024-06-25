using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Freights;

public sealed record Quantity
{
    public decimal Value { get; init; }

    private Quantity(decimal value) => Value = value;

    internal static Result<Quantity> Create(decimal value)
    {
        var isValid = value > 0;

        if (!isValid)
        {
            return FreightErrors.InvalidQuantity;
        }

        return new Quantity(value);
    }
}