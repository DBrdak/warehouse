using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Drivers.GetAllDrivers;

public sealed record GetAllDriversQuery() : IQuery<List<DriverModel>>;
