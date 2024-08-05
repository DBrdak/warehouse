using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Clients;

public sealed record NIP
{
    private static readonly Regex pattern = new(
        @"^ATU\d{8}|BE\d{10}|BG\d{9,10}|CY\d{8}L|CZ\d{8,10}|DE\d{9}|DK\d{8}|EE\d{9}|EL\d{9}|ES[A-Z0-9]\d{7}[A-Z0-9]|FI\d{8}|FR[A-HJ-NP-Z0-9]{2}\d{9}|HR\d{11}|HU\d{8}|IE\d{7}[A-W]|IT\d{11}|LT(\d{9}|\d{12})|LU\d{8}|LV\d{11}|MT\d{8}|NL\d{9}B\d{2}|PL\d{10}|PT\d{9}|RO\d{2,10}|SE\d{12}|SI\d{8}|SK\d{10}$");

    public string Value { get; init; }

    private NIP(string value) => Value = value;

    internal static Result<NIP> Create(string value)
    {
        var isValid = pattern.IsMatch(value);

        if (!isValid)
        {
            return ClientErrors.InvalidNIPError;
        }

        return new NIP(value);
    }
}