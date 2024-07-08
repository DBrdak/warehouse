﻿using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Clients;

public interface IClientRepository
{
    Task<Result<Client>> GetByIdAsync(ClientId clientId, CancellationToken cancellationToken);
    Task<Result<Client>> AddAsync(Client client, CancellationToken cancellationToken);
    Result<Client> Update(Client client);
    Result Remove(Client client);
}