using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Freights;

public sealed record FreightName
{
    private static readonly Regex pattern = new(@"^[\p{L}\p{N}\s\-\/\.,&!()""':\[\]{}+]{1,55}$");
    public string Value { get; init; }

    private FreightName(string value) => Value = value;

    internal static Result<FreightName> Create(string value)
    {
        var isValid = pattern.IsMatch(value);

        if (!isValid)
        {
            return FreightErrors.InvalidFreightName;
        }

        return new FreightName(value);
    }
}