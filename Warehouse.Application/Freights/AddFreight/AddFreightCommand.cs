using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Freights.AddFreight;

public sealed record AddFreightCommand() : ICommand<FreightModel>;
