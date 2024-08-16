using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Transports.HandleTransport;

public sealed record HandleTransportCommand(
    string Type,
    Guid WarehousemanId,
    Guid DriverId,
    Guid ClientId,
    DateTime? DateTime = null) : ICommand<TransportModel>;