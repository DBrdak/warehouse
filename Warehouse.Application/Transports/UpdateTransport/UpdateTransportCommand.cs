using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Transports.UpdateTransport;

public sealed record UpdateTransportCommand() : ICommand<TransportModel>;
