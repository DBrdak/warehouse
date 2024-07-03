using Warehouse.Application.Clients.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Clients.UpdateClient;

public sealed record UpdateClientCommand(Guid Id, string? NewName, string? NewNip) : ICommand<ClientModel>;
