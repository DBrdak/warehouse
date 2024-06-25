using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Freights;

public sealed record FreightType
{
    private static readonly Regex pattern = new(@"^[\p{L}\p{N}\s\-\/\.,&!()""':\[\]{}+]{1,55}$");
    public string Value { get; init; }

    private FreightType(string value) => Value = value;

    internal static Result<FreightType> Create(string value)
    {
        var isValid = pattern.IsMatch(value);

        if (!isValid)
        {
            return FreightErrors.InvalidFreightType;
        }

        return new FreightType(value);
    }
}