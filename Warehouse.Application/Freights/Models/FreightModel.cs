using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Application.Shared.Models;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Freights;

namespace Warehouse.Application.Freights.Models;

public sealed record FreightModel : BusinessModel<Freight, FreightId>
{
    public string Name { get; init; }
    public string Type { get; init; }
    public decimal Quantity { get; init; }
    public string Unit { get; init; }
    public PalletSpaceModel? PalletSpace { get; init; }
    public TransportModel? Import { get; init; }
    public TransportModel? Export { get; init; }

    private FreightModel(
        Guid id,
        string name,
        string type,
        decimal quantity,
        string unit,
        PalletSpaceModel? palletSpace,
        TransportModel? import,
        TransportModel? export) : base(id)
    {
        Name = name;
        Type = type;
        Quantity = quantity;
        Unit = unit;
        PalletSpace = palletSpace;
        Import = import;
        Export = export;
    }

    public static FreightModel FromDomainModel<TCaller>(Freight freight, bool? isImport = null) =>
        typeof(TCaller) switch
        {
            var callerType when callerType == typeof(PalletSpaceModel) => new(
                freight.Id.Id,
                freight.Name.Value,
                freight.Type.Value,
                freight.Quantity.Value,
                freight.Unit.Value,
                null,
                freight.Import is null ? null : TransportModel.FromDomainModel<FreightModel>(freight.Import),
                freight.Export is null ? null : TransportModel.FromDomainModel<FreightModel>(freight.Export)),
            var callerType when callerType == typeof(TransportModel) && isImport is true => new(
                freight.Id.Id,
                freight.Name.Value,
                freight.Type.Value,
                freight.Quantity.Value,
                freight.Unit.Value,
                freight.PalletSpace is null ? null : PalletSpaceModel.FromDomainModel<FreightModel>(freight.PalletSpace),
                null,
                freight.Export is null ? null : TransportModel.FromDomainModel<FreightModel>(freight.Export)),
            var callerType when callerType == typeof(TransportModel) && isImport is false => new(
                freight.Id.Id,
                freight.Name.Value,
                freight.Type.Value,
                freight.Quantity.Value,
                freight.Unit.Value,
                freight.PalletSpace is null ? null : PalletSpaceModel.FromDomainModel<FreightModel>(freight.PalletSpace),
                freight.Import is null ? null : TransportModel.FromDomainModel<FreightModel>(freight.Import),
                null),
            _ => FromDomainModel(freight)
        };

    public static FreightModel FromDomainModel(Freight freight) =>
        new(
            freight.Id.Id,
            freight.Name.Value,
            freight.Type.Value,
            freight.Quantity.Value,
            freight.Unit.Value,
            freight.PalletSpace is null ? null : PalletSpaceModel.FromDomainModel<FreightModel>(freight.PalletSpace),
            freight.Import is null ? null : TransportModel.FromDomainModel<FreightModel>(freight.Import),
            freight.Export is null ? null : TransportModel.FromDomainModel<FreightModel>(freight.Export));
}