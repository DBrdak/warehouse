using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Domain.Clients;

public sealed class Client : Entity<ClientId>
{
    public NIP Nip { get; private set; }
    public ClientName Name { get; private set; }
    private readonly List<Transport> _transports;
    public IReadOnlyCollection<Transport> Transports => _transports;

    private Client(NIP nip, ClientName name, List<Transport> transports, ClientId? id = null) : base(id)
    {
        Nip = nip;
        Name = name;
        _transports = transports;
    }

    public static Result<Client> Create(string nip, string name)
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

    public Result EditNIP(string nip)
    {
        var nipCreateResult = NIP.Create(nip);

        if (nipCreateResult.IsFailure)
        {
            return nipCreateResult.Error;
        }

        var clientNip = nipCreateResult.Value;

        Nip = clientNip;

        return Result.Success();
    }

    public Result EditName(string name)
    {
        var nameCreateResult = ClientName.Create(name);

        if (nameCreateResult.IsFailure)
        {
            return nameCreateResult.Error;
        }

        var clientName = nameCreateResult.Value;

        Name = clientName;

        return Result.Success();
    }

    internal Result BookTransport(Transport transport)
    {
        var isAlreadyBookedByClient = _transports.Any(t => t.Id == transport.Id);

        if (isAlreadyBookedByClient)
        {
            return ClientErrors.AlreadyBookedByClient;
        }

        var isAlreadyBookedByAnotherClient = transport.Client.Id != Id;

        if (isAlreadyBookedByAnotherClient)
        {
            return ClientErrors.AlreadyBookedByAnotherClient;
        }

        _transports.Add(transport);

        return Result.Success();
    }
}