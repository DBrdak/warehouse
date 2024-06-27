namespace Warehouse.Infrastructure.DataModels;

internal sealed class FreightDataModel
{
    public Guid Id { get; init; }
    public Guid PalletSpaceId { get; init; }
    public Guid ImportId { get; init; }
    public Guid? ExportId { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
    public decimal Quantity { get; init; }
    public string Unit { get; init; }
    public TransportDataModel Import { get; init; }
    public TransportDataModel? Export { get; init; }
    public PalletSpaceDataModel PalletSpace { get; init; }

    public FreightDataModel(
        Guid id,
        Guid palletSpaceId,
        Guid importId,
        Guid? exportId,
        string name,
        string type,
        decimal quantity,
        string unit,
        TransportDataModel import,
        TransportDataModel? export,
        PalletSpaceDataModel palletSpace)
    {
        Id = id;
        PalletSpaceId = palletSpaceId;
        ImportId = importId;
        ExportId = exportId;
        Name = name;
        Type = type;
        Quantity = quantity;
        Unit = unit;
        Import = import;
        Export = export;
        PalletSpace = palletSpace;
    }
}