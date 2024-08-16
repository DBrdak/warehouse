using Warehouse.Application.Clients.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Clients.GetAllClients;

public sealed record GetAllClientsQuery() : IQuery<List<ClientModel>>;
