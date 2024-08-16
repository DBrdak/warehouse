namespace Warehouse.Domain.Shared.Results;

public record Error(string Message)
{
    public static Error None = new(string.Empty);

    public static Error NullValue = new("Wartość NULL");
    public static Error InvalidValue = new("Nieprawidłowe dane wejściowe");
    public static Error Exception = new("Nieznany błąd");
    public static Error ValidationError(
        IEnumerable<string?> members) =>
        new($"Walidacja nie powiodła się dla: {string.Join(',', members)}");

    /// <summary>
    /// Use only for logging, do not return this error to user
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static Error ExceptionWithMessage(Exception e) => new($"{e.Message}");
    public static Error ExceptionWithMessage(string eMessage) => new($"{eMessage}");
    public override string ToString() => Message;
}