using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Clients.UpdateClient;

internal static class UpdateDriverErrors
{
    public static readonly Error InvalidRequest = new("Wprowadzono nieprawidłowe dane dla aktualizacji kontrahenta");
}