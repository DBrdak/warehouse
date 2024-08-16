using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Freights.ReceiveFreight;

public sealed record ReceiveFreightCommand(
    Guid ImportId,
    Guid PalletSpaceId,
    string Name,
    string Type,
    decimal Quantity,
    string Unit) : ICommand<FreightModel>;
