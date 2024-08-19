using Warehouse.Application.Sectors.Models;
using Warehouse.Application.Shared.Models;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Application.Warehousemen.Models;

public record WarehousemanModel : BusinessModel<Warehouseman, WarehousemanId>
{
    public int IdentificationNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? Position { get; init; }
    public SectorModel? Sector { get; init; }
    public IReadOnlyCollection<TransportModel>? Transports { get; init; }

    protected WarehousemanModel(
        Guid id,
        int identificationNumber,
        string firstName,
        string lastName,
        string? position,
        SectorModel? sector,
        IReadOnlyCollection<TransportModel>? transports) : base(id)
    {
        IdentificationNumber = identificationNumber;
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Sector = sector;
        Transports = transports;
    }

    public static WarehousemanModel FromDomainModel<TCaller>(Warehouseman warehouseman) =>
        typeof(TCaller) switch
        {
            var callerType when callerType == typeof(SectorModel) =>
                new(
                    warehouseman.Id.Id,
                    warehouseman.IdentificationNumber.Value,
                    warehouseman.FirstName.Value,
                    warehouseman.LastName.Value,
                    warehouseman.Position?.Value,
                    null,
                    warehouseman.Transports?.Select(TransportModel.FromDomainModel<WarehousemanModel>).ToList()),
            var callerType when callerType == typeof(PalletSpace) =>
                new(
                    warehouseman.Id.Id,
                    warehouseman.IdentificationNumber.Value,
                    warehouseman.FirstName.Value,
                    warehouseman.LastName.Value,
                    warehouseman.Position?.Value,
                    null,
                    null),
            var callerType when callerType == typeof(TransportModel) =>
                new(
                    warehouseman.Id.Id,
                    warehouseman.IdentificationNumber.Value,
                    warehouseman.FirstName.Value,
                    warehouseman.LastName.Value,
                    warehouseman.Position?.Value,
                    null,
                    null),
            _ => FromDomainModel(warehouseman)
        };

    public static WarehousemanModel FromDomainModel(Warehouseman warehouseman) =>
        new(
            warehouseman.Id.Id,
            warehouseman.IdentificationNumber.Value,
            warehouseman.FirstName.Value,
            warehouseman.LastName.Value,
            warehouseman.Position?.Value,
            SectorModel.FromDomainModel<WarehousemanModel>(warehouseman.Sector),
            warehouseman.Transports?.Select(TransportModel.FromDomainModel<WarehousemanModel>).ToList());

}