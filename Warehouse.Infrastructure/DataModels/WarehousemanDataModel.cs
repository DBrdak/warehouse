using Warehouse.Domain.Transports;

namespace Warehouse.Infrastructure.DataModels;

internal class WarehousemanDataModel
{
    public Guid Id { get; init; }
    public Guid SectorId { get; init; }
    public int IdNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Position { get; init; }
    public SectorDataModel Sector { get; init; }
    public ICollection<TransportDataModel> Transports { get; init; }

    public WarehousemanDataModel(
        Guid id,
        Guid sectorId,
        int idNumber,
        string firstName,
        string lastName,
        string position,
        SectorDataModel sector,
        ICollection<TransportDataModel> transports)
    {
        Id = id;
        SectorId = sectorId;
        IdNumber = idNumber;
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Sector = sector;
        Transports = transports;
    }
}