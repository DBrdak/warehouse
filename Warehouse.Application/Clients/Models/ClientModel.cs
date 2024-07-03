using System.Diagnostics.CodeAnalysis;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Clients.Models;

public sealed record ClientModel
{
    public string Name { get; init; }
    public string Nip { get; init; }
    public IReadOnlyCollection<TransportModel>? Transports { get; init; }

    private ClientModel(string name, string nip, IReadOnlyCollection<TransportModel>? transports)
    {
        Name = name;
        Nip = nip;
        Transports = transports;
    }

    internal static ClientModel FromDomainModel<TCaller>(Client client) =>
        typeof(TCaller) switch
        {
            var callerType when callerType == typeof(TransportModel) => new(
                client.Name.Value,
                client.Nip.Value,
                null),
            _ => FromDomainModel(client)
        };

    internal static ClientModel FromDomainModel(Client client) =>
        new(
            client.Name.Value,
            client.Nip.Value,
            client.Transports.Select(TransportModel.FromDomainModel<ClientModel>).ToList());
}