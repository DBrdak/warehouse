using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Shared;

public sealed record LastName
{
    private static readonly Regex pattern = new(@"^[\p{L}\p{M}][\p{L}\p{M}\s'-]{0,54}$");
    private static readonly Error invalidValueError = new("Nieprawidłowe nazwisko");
    public string Value { get; init; }

    private LastName(string value) => Value = value;

    internal static Result<LastName> Create(string value)
    {
        var isValid = pattern.IsMatch(value);

        if (!isValid)
        {
            return invalidValueError;
        }

        value = string.Concat(char.ToUpper(value[0]), value[1..]);

        return new LastName(value);
    }
}