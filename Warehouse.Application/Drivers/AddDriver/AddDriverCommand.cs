using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Drivers.AddDriver;

public sealed record AddDriverCommand() : ICommand<DriverModel>;