using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Drivers.UpdateDriver;

public sealed record UpdateDriverCommand() : ICommand<DriverModel>;
