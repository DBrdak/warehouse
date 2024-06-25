using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Domain.Clients;

public sealed class Client : Entity<ClientId>
{
    public NIP Nip { get; init; }
    public ClientName Name { get; init; }
    private readonly List<Transport> _transports;
    public IReadOnlyCollection<Transport> Transports => _transports;

    private Client(NIP nip, ClientName name, List<Transport> transports, ClientId? id = null) : base(id)
    {
        Nip = nip;
        Name = name;
        _transports = transports;
    }

    internal static Result<Client> Create(string nip, string name)
    {
        var clientNameCreateResult = ClientName.Create(name);

        if (clientNameCreateResult.IsFailure)
        {
            return clientNameCreateResult.Error;
        }

        var nipCreateResult = NIP.Create(nip);

        if (nipCreateResult.IsFailure)
        {
            return nipCreateResult.Error;
        }

        var clientName = clientNameCreateResult.Value;
        var clientNip = nipCreateResult.Value;

        return new Client(clientNip, clientName, []);
    }
}