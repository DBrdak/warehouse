using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Freights;

public sealed record Unit
{
    private static readonly Regex pattern = new(@"^[\p{L}\p{N}\s\-\/\.,&!()""':\[\]{}+]{1,55}$");
    public string Value { get; init; }

    private Unit(string value) => Value = value;

    internal static Result<Unit> Create(string value)
    {
        var isValid = pattern.IsMatch(value);

        if (!isValid)
        {
            return FreightErrors.InvalidUnit;
        }

        return new Unit(value);
    }
}