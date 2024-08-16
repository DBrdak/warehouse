using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Transports.RemoveTransport;

public sealed record RemoveTransportCommand(Guid Id) : ICommand;
