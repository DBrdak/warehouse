using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data.DataModels.Shared;

namespace Warehouse.Infrastructure.Data.DataModels;

internal sealed class ClientDataModel : IDataModel<Client>
{
    public Guid Id { get; init; }
    public string NIP { get; init; }
    public string Name { get; init; }
    public IEnumerable<TransportDataModel> Transports { get; init; }

    private ClientDataModel(Guid id, string nip, string name, IEnumerable<TransportDataModel> transports)
    {
        Id = id;
        NIP = nip;
        Name = name;
        Transports = transports;
    }

    internal static ClientDataModel FromDomainModel(Client domainModel) =>
        new(
            domainModel.Id.Id,
            domainModel.Nip.Value,
            domainModel.Name.Value,
            domainModel.Transports.Select(TransportDataModel.FromDomainModel));

    public Client ToDomainModel()
    {
        return null;
    }
}
