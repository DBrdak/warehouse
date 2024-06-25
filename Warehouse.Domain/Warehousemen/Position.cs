using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Warehousemen;

public sealed record Position
{
    private static readonly Regex pattern = new(@"^[\p{L}\p{N}\s\-\/\.,&!()""':\[\]{}+]{1,55}$");
    public string Value { get; init; }

    private Position(string value) => Value = value;

    internal static Result<Position> Create(string value)
    {
        var isValid = pattern.IsMatch(value);

        if (!isValid)
        {
            return WarehousemanErrors.InvalidWarehousemanPosition;
        }

        return new Position(value);
    }
}