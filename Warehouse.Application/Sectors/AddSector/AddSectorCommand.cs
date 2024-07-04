using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Sectors.AddSector;

public sealed record AddSectorCommand(int Number) : ICommand<SectorModel>;
