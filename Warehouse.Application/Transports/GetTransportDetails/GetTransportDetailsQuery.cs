using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Transports.GetTransportDetails;

public sealed record GetTransportDetailsQuery(Guid TransportId) : IQuery<TransportModel>;
