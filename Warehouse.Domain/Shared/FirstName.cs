using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Shared;

public sealed record FirstName
{
    private static readonly Regex pattern = new(@"^[\p{L}\p{M}][\p{L}\p{M}\s'-]{0,54}$");
    private static readonly Error invalidValueError = new("Nieprawidłowe imię");
    public string Value { get; init; }

    private FirstName(string value) => Value = value;

    internal static Result<FirstName> Create(string value)
    {
        var isValid = pattern.IsMatch(value);

        if (!isValid)
        {
            return invalidValueError;
        }

        value = string.Concat(char.ToUpper(value[0]), value[1..]);

        return new FirstName(value);
    }
}