using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Transports.HandleTransport;

public sealed record HandleTransportCommand(
    int Number,
    string Type,
    DateTime? DateTime,
    Guid WarehousemanId,
    Guid DriverId,
    Guid ClientId) : ICommand<TransportModel>;