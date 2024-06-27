namespace Warehouse.Infrastructure.DataModels;

internal sealed class ClientDataModel
{
    public Guid Id { get; init; }
    public string NIP { get; init; }
    public string Name { get; init; }
    public ICollection<TransportDataModel> Transports { get; init; }

    public ClientDataModel(Guid id, string nip, string name, ICollection<TransportDataModel> transports)
    {
        Id = id;
        NIP = nip;
        Name = name;
        Transports = transports;
    }
}
