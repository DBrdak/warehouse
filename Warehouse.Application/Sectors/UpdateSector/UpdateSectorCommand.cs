using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Sectors.UpdateSector;

public sealed record UpdateSectorCommand() : ICommand<SectorModel>;
