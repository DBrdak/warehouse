using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Clients;

public sealed record ClientName
{
    private static readonly Regex regex = new (@"^[\p{L}\s\-\/\.,&!()""':\[\]{}+]{1,55}$");
    public string Value { get; init; }

    private ClientName(string value) => Value = value;

    internal static Result<ClientName> Create(string value)
    {
        var isValid = regex.IsMatch(value);

        if (!isValid)
        {
            return ClientErrors.InvalidNIPError;
        }

        return new ClientName(value);
    }
}