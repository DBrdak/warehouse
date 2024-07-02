using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Transports.AddTransport;

public sealed record AddTransportCommand() : ICommand<TransportModel>;
