using Warehouse.Application.Shared.Models;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Clients;

namespace Warehouse.Application.Clients.Models;

public sealed record ClientModel : BusinessModel<Client,ClientId>
{
    public string Name { get; init; }
    public string Nip { get; init; }
    public IReadOnlyCollection<TransportModel>? Transports { get; init; }

    public ClientModel() : base(Guid.NewGuid())
    {
        Name = "";
        Nip = "";
    }

    private ClientModel(Guid id, string name, string nip, IReadOnlyCollection<TransportModel>? transports) : base(id)
    {
        Name = name;
        Nip = nip;
        Transports = transports;
    }

    internal static ClientModel FromDomainModel<TCaller>(Client client) =>
        typeof(TCaller) switch
        {
            var callerType when callerType == typeof(TransportModel) => new(
                client.Id.Id,
                client.Name.Value,
                client.Nip.Value,
                null),
            _ => FromDomainModel(client)
        };

    internal static ClientModel FromDomainModel(Client client) =>
        new(
            client.Id.Id,
            client.Name.Value,
            client.Nip.Value,
            client.Transports?.Select(TransportModel.FromDomainModel<ClientModel>).ToList());

    public ClientModel? Copy() => new (Id, Name, Nip, Transports);
}