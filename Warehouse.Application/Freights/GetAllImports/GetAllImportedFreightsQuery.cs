using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Freights.GetAllImports;

public sealed record GetAllImportedFreightsQuery() : IQuery<List<FreightModel>>;
