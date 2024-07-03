using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Infrastructure.Utils;

internal static class DataAccessErrors
{
    public static Error NotFound<TEntity>() =>
        new($"Nie znaleziono {typeof(TEntity).Name}");
}