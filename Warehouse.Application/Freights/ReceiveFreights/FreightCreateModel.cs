namespace Warehouse.Application.Freights.ReceiveFreights;

public sealed record FreightCreateModel(
    int SectorNumber,
    int RackNumber,
    int ShelfNumber,
    int PalletSpaceNumber,
    string Name,
    string Type,
    decimal Quantity,
    string Unit)
{
    public static FreightCreateModel Init() =>
        new(
            -1,
            -1,
            -1,
            -1,
            string.Empty,
            string.Empty,
            0,
            string.Empty);
}