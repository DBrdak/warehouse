using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Sectors.GetSectors;

public sealed record GetSectorsQuery() : IQuery<List<SectorModel>>;
