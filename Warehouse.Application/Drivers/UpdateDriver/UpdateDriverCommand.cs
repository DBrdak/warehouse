using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Drivers.UpdateDriver;

public sealed record UpdateDriverCommand(
    Guid Id,
    string NewFirstName,
    string NewLastName,
    string NewVehiclePlate) : ICommand<DriverModel>;
