using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Drivers.AddDriver;

public sealed record AddDriverCommand(string FirstName, string LastName, string VehiclePlate) : ICommand<DriverModel>;