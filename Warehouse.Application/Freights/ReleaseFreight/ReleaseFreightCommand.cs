using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Freights.ReleaseFreight;

public sealed record ReleaseFreightCommand(Guid ExportId, IEnumerable<FreightModel> Freights)
    : ICommand;
