using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Clients.RemoveClient;

public sealed record RemoveClientCommand(Guid ClientId) : ICommand;
