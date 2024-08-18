using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Freights.ReceiveFreights;

public sealed record ReceiveFreightsCommand(Guid ImportId, IEnumerable<FreightCreateModel> Freights)
    : ICommand;
