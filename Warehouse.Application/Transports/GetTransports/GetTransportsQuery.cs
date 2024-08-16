using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Transports.GetTransports;

public sealed record GetTransportsQuery(string Type) : IQuery<List<TransportModel>>;
