using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Sectors.AddSector;

public sealed record AddSectorCommand(int SectorNumber, IEnumerable<SectorRackAddModel> Racks) : ICommand<SectorModel>;

public sealed record SectorRackAddModel(int RackNumber, IEnumerable<SectorRackShelfAddModel> Shelfs);

public sealed record SectorRackShelfAddModel(int ShelfNumber, int PalletSpaceCount);
