using Warehouse.Domain.Shared.Results;

namespace Warehouse.Infrastructure.Utils;

internal static class DataAccessErrors
{
    public static Error NotFound<TEntity>() =>
        new($"Nie znaleziono {typeof(TEntity).Name}");
}