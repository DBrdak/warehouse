﻿using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Freights;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Freights.Models;

public sealed record FreightModel
{
    public string Name { get; init; }
    public string Type { get; init; }
    public decimal Quantity { get; init; }
    public string Unit { get; init; }
    public PalletSpaceModel? PalletSpace { get; init; }
    public TransportModel? Import { get; init; }
    public TransportModel? Export { get; init; }

    private FreightModel(
        string name,
        string type,
        decimal quantity,
        string unit,
        PalletSpaceModel? palletSpace,
        TransportModel? import,
        TransportModel? export)
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
                freight.Name.Value,
                freight.Type.Value,
                freight.Quantity.Value,
                freight.Unit.Value,
                null,
                TransportModel.FromDomainModel<FreightModel>(freight.Import),
                TransportModel.FromDomainModel<Freight>(freight.Export)),
            var callerType when callerType == typeof(TransportModel) && isImport is true => new(
                freight.Name.Value,
                freight.Type.Value,
                freight.Quantity.Value,
                freight.Unit.Value,
                PalletSpaceModel.FromDomainModel<FreightModel>(freight.PalletSpace),
                null,
                TransportModel.FromDomainModel<Freight>(freight.Export)),
            var callerType when callerType == typeof(PalletSpaceModel) && isImport is false => new(
                freight.Name.Value,
                freight.Type.Value,
                freight.Quantity.Value,
                freight.Unit.Value,
                PalletSpaceModel.FromDomainModel<FreightModel>(freight.PalletSpace),
                TransportModel.FromDomainModel<FreightModel>(freight.Import),
                null),
            _ => FromDomainModel(freight)
        };

    public static FreightModel FromDomainModel(Freight freight) =>
        new(
            freight.Name.Value,
            freight.Type.Value,
            freight.Quantity.Value,
            freight.Unit.Value,
            PalletSpaceModel.FromDomainModel<FreightModel>(freight.PalletSpace),
            TransportModel.FromDomainModel<FreightModel>(freight.Import),
            TransportModel.FromDomainModel<Freight>(freight.Export));
}