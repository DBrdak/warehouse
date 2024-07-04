using Warehouse.Application.Freights.Models;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Shared.Messaging;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Freights.AddFreight;

public sealed record AddFreightCommand(
    Guid ImportId,
    Guid PalletSpaceId,
    string Name,
    string Type,
    decimal Quantity,
    string Unit) : ICommand<FreightModel>;
