using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Sectors.GetSectors;

public sealed record GetSectorsQuery(
    GetSectorQueryType QueryType = GetSectorQueryType.Default) : IQuery<List<SectorModel>>;

public enum GetSectorQueryType
{
    Default,
    IncludePalletSpaces,
    Detailed
}
