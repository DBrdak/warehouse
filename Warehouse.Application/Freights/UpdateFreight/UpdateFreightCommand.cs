using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Freights.UpdateFreight;

public sealed record UpdateFreightCommand() : ICommand<FreightModel>;
